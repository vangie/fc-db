using System;
using System.IO;
using System.Text;
using Aliyun.Serverless.Core;
using Npgsql;

namespace PostgreSQL
{
 public class MssqlHandler
    {     
        private static String server = Environment.GetEnvironmentVariable("HOST");
        private static String port = Environment.GetEnvironmentVariable("PORT");
        private static String database = Environment.GetEnvironmentVariable("DATABASE");
        private static String user = Environment.GetEnvironmentVariable("USER");
        private static String password = Environment.GetEnvironmentVariable("PASSWORD");
        private static String connStr = $"Host={server},{port};Database={database};Username={user};Password={password}"; 

        public void Initializer(IFcContext context)
        {
            Console.WriteLine(connStr);
            NpgsqlConnection myConn = null;
            try
            {
                myConn = new NpgsqlConnection(connStr);
                myConn.Open();
                var cmd = new NpgsqlCommand();
                cmd.Connection = myConn;

                // Insert some data
                cmd.CommandText = "CREATE TABLE USERS (ID INT PRIMARY KEY  NOT NULL, NAME  TEXT  NOT NULL, AGE  INT  NOT NULL, ADDRESS  CHAR(50), SALARY  REAL)";
                cmd.ExecuteNonQuery();
                Console.WriteLine("表创建成功！");

                cmd.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (myConn != null)
                {
                    myConn.Close();
                }
            }

        }
        public Stream Handler(Stream input, IFcContext context)
        {
            NpgsqlConnection myConn = null;
            try
            {
                myConn = new NpgsqlConnection(connStr);
                myConn.Open();
                var cmd = new NpgsqlCommand();
                cmd.Connection = myConn;

                // Insert some data
                cmd.CommandText ="INSERT INTO USERS (ID,NAME,AGE,ADDRESS,SALARY)  VALUES (1, 'TEST', 32, 'California', 50000.00 )";
                if (cmd.ExecuteNonQuery() > 0) 
                {
                    Console.WriteLine("数据插入成功！");
                }
                cmd = new NpgsqlCommand("SELECT * FROM users", myConn);
                var rdr = cmd.ExecuteReader();

                String res = "";
                while(rdr.Read()){
                    res += $", {rdr.GetString(1)}";
                }
                res = res.Substring(1, res.Length - 1);
                cmd.Dispose();

                return new MemoryStream(Encoding.UTF8.GetBytes(res));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (myConn != null)
                {
                    myConn.Close();
                }
            }
        }
    }
}
