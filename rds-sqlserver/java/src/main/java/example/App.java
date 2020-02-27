package example;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import com.aliyun.fc.runtime.Context;
import com.aliyun.fc.runtime.StreamRequestHandler;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;

public class App implements StreamRequestHandler{

    @Override
    public void handleRequest(
            InputStream inputStream, OutputStream outputStream, Context context) throws IOException {
        byte[] bytes = new byte[0];
        bytes = new byte[inputStream.available()];
        inputStream.read(bytes);
        String str = new String(bytes);
        System.out.println(str);
        String currentTime = "unavailable";
        String user = "txd123";
        String password = "Txd1231512315";
        Connection conn;
        Statement stmt;
        ResultSet rs;
        String url = "jdbc:sqlserver://rm-bp102b430827dg2dv.sqlserver.rds.aliyuncs.com:1433;DatabaseName=TestDB;";
        String sql = "SELECT * FROM inventory WHERE quantity > 152";
        try {
            // 连接数据库
            conn = DriverManager.getConnection(url, user, password);
            // 建立Statement对象
            stmt = conn.createStatement();
            // 执行数据库查询语句
            rs = stmt.executeQuery(sql);
            while (rs.next()) {
                String name = rs.getString("name");
                int quantity = rs.getInt("quantity");
                int id  = rs.getInt("id");
                
               System.out.println("id:"+id+"; name:"+name+"; quantity:"+quantity);
            }
            if (rs != null) {
                rs.close();
                rs = null;
            }
            if (stmt != null) {
                stmt.close();
                stmt = null;
            }
            if (conn != null) {
                conn.close();
                conn = null;
            }
        } catch (SQLException e) {
            e.printStackTrace();
            System.out.println("数据库连接失败");
        }
    

        outputStream.write(currentTime.getBytes());

    }

}
