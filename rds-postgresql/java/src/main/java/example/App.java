package example;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import com.aliyun.fc.runtime.Context;
import com.aliyun.fc.runtime.FunctionInitializer;
import com.aliyun.fc.runtime.StreamRequestHandler;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

public class App implements StreamRequestHandler, FunctionInitializer {

    private final String server = System.getenv("HOST");
    private final String port = System.getenv("PORT");
    private final String dbName = System.getenv("DATABASE");
    private final String user = System.getenv("USER");
    private final String passwd = System.getenv("PASSWORD");

    private String url;

    {
        url = String.format("jdbc:postgresql://%s:%s/%s", server, port, dbName);
    }
    @Override
    public void initialize(final Context context) {
        conditionallyCreateUsersTable();
    }

    @Override
    public void handleRequest(final InputStream inputStream, final OutputStream outputStream, final Context context)
            throws IOException {

        final String currentTime = "successfully！";
        try (Connection conn = getConnection()) {
            final Statement stmt = conn.createStatement();
            final String sql = "INSERT INTO COMPANY01 (ID,NAME,AGE,ADDRESS,SALARY) VALUES (1, 'Paul', 32, 'California', 20000.00 )";
            stmt.executeUpdate(sql);
            System.out.println("========数据插入成功！=========");

            final ResultSet rs = stmt.executeQuery("select * from company01");
            System.out.println("===========查询数据成功！==========");
			while(rs.next()){
				final int id = rs.getInt("id");
				final String name = rs.getString("name");
				final int age = rs.getInt("age");
				final String address = rs.getString("address");
				final float salary = rs.getFloat("salary");
				System.out.println(id + "," + name + "," + age + "," + address.trim() + "," + salary);
			}
	        stmt.close();
	        conn.close();

        }catch (final Exception e) {
            e.printStackTrace();
	        System.err.println(e.getClass().getName()+": "+e.getMessage());
	        System.exit(0);
        }
        

        outputStream.write(currentTime.getBytes());

    }

    private Connection getConnection() throws SQLException {
        return DriverManager.getConnection(url, user, passwd);
    }

    private void conditionallyCreateUsersTable() {
        Statement stmt = null;
        try (Connection conn = getConnection()) {
            stmt = conn.createStatement();
            final String sql = "CREATE TABLE COMPANY01 (ID INT PRIMARY KEY  NOT NULL, NAME  TEXT  NOT NULL, AGE  INT  NOT NULL, ADDRESS  CHAR(50), SALARY  REAL)";
            stmt.executeUpdate(sql);
	        stmt.close();
	        conn.close();

        }catch (final Exception e) {
            e.printStackTrace();
	        System.err.println(e.getClass().getName()+": "+e.getMessage());
	        System.exit(0);
        }
        
    }

}
