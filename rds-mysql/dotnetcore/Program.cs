using System;
using System.IO;
using System.Text;
using Aliyun.Serverless.Core;
using MySql.Data.MySqlClient;

namespace mysql
{
    public class MysqlHandler
    {     
        private static String host = Environment.GetEnvironmentVariable("MYSQL_HOST");
        private static String port = Environment.GetEnvironmentVariable("MYSQL_PORT");
        private static String dbName = Environment.GetEnvironmentVariable("MYSQL_DBNAME");
        private static String user = Environment.GetEnvironmentVariable("MYSQL_USER");
        private static String passwd = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
        private static string connStr = $"Host={host};Port={port};Username={user};Password={passwd};Database={dbName}";

        public void Initializer(IFcContext context)
        {
            MySqlConnection myConn = null;
            try
            {
                myConn = new MySqlConnection(connStr);
                myConn.Open();

                MySqlCommand myCmd = new MySqlCommand(@"CREATE TABLE IF NOT EXISTS users (
                    id  VARCHAR(64) NOT NULL,
                    name    VARCHAR(128) NOT NULL,
                    PRIMARY KEY(id))", myConn);
                myCmd.ExecuteNonQuery();

                Console.WriteLine("表创建成功！");

                myCmd.Dispose();

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
            MySqlConnection myConn = null;
            try
            {
                myConn = new MySqlConnection(connStr);
                myConn.Open();
                MySqlCommand myCmd = new MySqlCommand("REPLACE INTO users (`id`,`name`) values('5','csharp')", myConn);
                if (myCmd.ExecuteNonQuery() > 0) 
                {
                    Console.WriteLine("数据插入成功！");
                }
                myCmd.Dispose();

                myCmd = new MySqlCommand("SELECT * FROM users", myConn);
                MySqlDataReader rdr = myCmd.ExecuteReader();

                String res = "";
                while(rdr.Read()){
                    res += $", {rdr.GetString(1)}";
                }
                res = res.Substring(1, res.Length - 1);
                myCmd.Dispose();

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