# 函数计算访问数据库 dotnetcore2.1 示例工程


示例包括如下两部分内容：

1. 在 initializer 中创建数据表

    ```
    MySqlConnection myConnnect = new MySqlConnection(constructorString);  
    myConnnect.Open();  
    MySqlCommand myCmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS users (id  VARCHAR(64) NOT NULL,name    VARCHAR(128) NOT NULL,PRIMARY KEY(id))", myConnnect);
    Console.WriteLine(myCmd.ExecuteNonQuery()); 
    Console.WriteLine("表创建成功！"); 
    ```

2. 在 handler 中插入数据

    ```
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
    ```

## 构建
```
dotnet publish -c Release
```
## 部署

```
fun deploy
```