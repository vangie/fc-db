using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Aliyun.Serverless.Core;

namespace sqlserver
{
 public class MssqlHandler
    {     
        private static String server = Environment.GetEnvironmentVariable("MSSQL_SERVER");
        private static String port = Environment.GetEnvironmentVariable("MSSQL_PORT");
        private static String database = Environment.GetEnvironmentVariable("MSSQL_DATABASE");
        private static String user = Environment.GetEnvironmentVariable("MSSQL_USER");
        private static String password = Environment.GetEnvironmentVariable("MSSQL_PASSWORD");
        private static String connStr = $"Data Source={server},{port};User Id={user};Password={password};Initial Catalog={database};"; 

        public void Initializer(IFcContext context)
        {
            SqlConnection myConn = null;
            try
            {
                myConn = new SqlConnection(connStr);
                myConn.Open();

                SqlCommand myCmd = new SqlCommand(@"
                if not exists(select * from sysobjects where name='users' and xtype='U')
                create table users (
                    id varchar(64) not null,
                    name varchar(128) not null,
                    PRIMARY KEY(id)
                )", myConn);
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
            SqlConnection myConn = null;
            try
            {
                myConn = new SqlConnection(connStr);
                myConn.Open();
                SqlCommand myCmd = new SqlCommand(@"
                merge users as target
                using (values('csharp')) 
                    as source(name)
                    on target.id = '5'
                when matched then
                    update
                    set name = source.name
                when not matched then
                    insert (id, name)
                    values( '5', source.name);
                ", myConn);
                if (myCmd.ExecuteNonQuery() > 0) 
                {
                    Console.WriteLine("数据插入成功！");
                }
                myCmd.Dispose();

                myCmd = new SqlCommand("SELECT * FROM users", myConn);
                SqlDataReader rdr = myCmd.ExecuteReader();

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
