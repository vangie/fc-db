package example;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import com.aliyun.fc.runtime.Context;
import com.aliyun.fc.runtime.FunctionComputeLogger;
import com.aliyun.fc.runtime.FunctionInitializer;
import com.aliyun.fc.runtime.StreamRequestHandler;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;

public class App implements StreamRequestHandler, FunctionInitializer {

    private String server = System.getenv("MSSQL_SERVER");
    private String port = System.getenv("MSSQL_PORT");
    private String dbName = System.getenv("MSSQL_DATABASE");
    private String user = System.getenv("MSSQL_USER");
    private String passwd = System.getenv("MSSQL_PASSWORD");

    private String url;

    {
        url = String.format("jdbc:sqlserver://%s:%s;DatabaseName=%s;", server, port, dbName);
    }

    @Override
    public void initialize(Context context) {
        conditionallyCreateUsersTable();
    }

    @Override
    public void handleRequest(
            InputStream inputStream, OutputStream outputStream, Context context) throws IOException {

        FunctionComputeLogger logger = context.getLogger();


        String currentTime = "unavailable";

        try (Connection conn = getConnection()) {

            Statement stmt = conn.createStatement();
            ResultSet resultSet = stmt.executeQuery("SELECT GETDATE();");

            if (resultSet.next()) {
                currentTime = resultSet.getObject(1).toString();
            }

            logger.info("Successfully executed query.  Current: " + currentTime);

            String sql = "merge users as target " +
                "using (values(?)) " +
                "    as source(name) " +
                "    on target.id = ? " +
                "when matched then " +
                "    update " +
                "    set name = source.name " +
                "when not matched then "+
                "    insert (id, name) " +
                "    values( ?, source.name);";

            PreparedStatement ps = conn.prepareStatement(sql);
            ps.setString(1, "du");
            ps.setString(2, "3");
            ps.setString(3, "3");
            
            ps.execute();

            resultSet = stmt.executeQuery("SELECT * FROM users");

            while (resultSet.next()) {
                logger.info("user: " + resultSet.getString(2));
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }

        outputStream.write(currentTime.getBytes());

    }

    private Connection getConnection() throws SQLException {
        return DriverManager.getConnection(url, user, passwd);
    }

    private void conditionallyCreateUsersTable() {

        String sql = "if not exists(select * from sysobjects where name='users' and xtype='U')\n" +
                "   create table users (\n" +
                "   id varchar(64) not null,\n" +
                "   name varchar(128) not null,\n" +
                "   PRIMARY KEY(id)\n" +
                ")";

        try (Connection conn = getConnection()) {

            Statement stmt = conn.createStatement();
            stmt.executeUpdate(sql);

        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

}
