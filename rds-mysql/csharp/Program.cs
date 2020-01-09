using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Aliyun.Serverless.Core;
using Microsoft.Extensions.Logging;
using MySql.Data;  
using MySql.Data.MySqlClient;  
namespace mysql
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    public class MysqlHandler
     {     
            private static String host = Environment.GetEnvironmentVariable("MYSQL_HOST");
            private static String dbName = Environment.GetEnvironmentVariable("MYSQL_DBNAME");
            private static String user = Environment.GetEnvironmentVariable("MYSQL_USER");
            private static String passwd = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            private static string constructorString = "server=" + host + ";"+"User Id=" + user+";"+"password=" + passwd+";"+"Database="+dbName ;
        
        public void Init(IFcContext context){
            try{
            MySqlConnection myConnnect = new MySqlConnection(constructorString);  
            myConnnect.Open();  
            MySqlCommand myCmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS users (id  VARCHAR(64) NOT NULL,name    VARCHAR(128) NOT NULL,PRIMARY KEY(id))", myConnnect);
            Console.WriteLine(myCmd.ExecuteNonQuery()); 
            Console.WriteLine("表创建成功！"); 
             
            myCmd.Dispose();  
            myConnnect.Close(); 
            }catch (Exception ex){
                throw ex;
            }     

        }
        public Stream InsertData(Stream input, IFcContext context)
        {
            try{
            MySqlConnection myConnnect = new MySqlConnection(constructorString);  
            myConnnect.Open();  
            MySqlCommand myCmd = new MySqlCommand("insert `mydb`.`users`(`id`,`name`) values('1','too')", myConnnect);  
            Console.WriteLine(myCmd.CommandText);
            if (myCmd.ExecuteNonQuery() > 0)  
            {  
                Console.WriteLine("数据插入成功！"); 
                 
            } 
            myCmd.Dispose();  
            myConnnect.Close(); 
            }catch (Exception ex){
                throw ex;
            }  
                      
            byte[] hello = Encoding.UTF8.GetBytes("hello world");
            MemoryStream output = new MemoryStream();
            output.Write(hello, 0, hello.Length);
            output.Seek(0, SeekOrigin.Begin);
            return output;

        }

     }
}
