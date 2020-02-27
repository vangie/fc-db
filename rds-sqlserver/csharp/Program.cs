using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Aliyun.Serverless.Core;

namespace sqlserver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    public class Handler
     {
        public string Sqlconn = "Data Source=rm-bp102b430827dg2dv.sqlserver.rds.aliyuncs.com;User ID=txd123;Password=Txd1231512315;Initial Catalog=TestDB;";     
        public Stream QueryData(Stream input, IFcContext context)
        {
            SqlConnection conn = new SqlConnection(Sqlconn);
            conn.Open();
            string SqlStr = "SELECT * FROM inventory WHERE quantity > 152";
            SqlCommand SqlCmd = new SqlCommand(SqlStr, conn);
            SqlDataReader SqlData = SqlCmd.ExecuteReader();
            //判断是否查询到有数据：
            if(!SqlData.Read())
                Console.Write("查询无结果！");
            else
            {           
                //获取查询到的内容：
                string name = SqlData["name"].ToString();
                Console.Write("查询到的用户名是：" + name);
            }
            //关闭数据库：
            conn.Close();

            byte[] hello = Encoding.UTF8.GetBytes("hello world");
            MemoryStream output = new MemoryStream();
            output.Write(hello, 0, hello.Length);
            output.Seek(0, SeekOrigin.Begin);
            return output;

        }

     }
}
