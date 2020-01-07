# 函数计算访问数据库 Python 示例工程

示例包括如下两部分内容：

1. 在 initializer 中创建数据表

    ```php
    try {
        $conn = new PDO("mysql:host=$servername;dbname=$dbname",$username,$password);
        $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

        $sql = "CREATE TABLE IF NOT EXISTS users (
            id        VARCHAR(64) NOT NULL,
            name    VARCHAR(128) NOT NULL,
            PRIMARY KEY(id))";
        $conn->exec($sql);
        echo "数据表 MyGuests 创建成功";
        echo "\r\n";
    }catch(PDOException $e){
        echo $e->getMessage();
    }
    ```

2. 在 handler 中插入并查询数据

    ```php
    try {
      $GLOBALS['conn']->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
  
      $GLOBALS['conn']->beginTransaction();
      $GLOBALS['conn']->exec("insert `mydb`.`users`(`id`,`name`) values('1','too')");
      $row=$GLOBALS['conn']->commit();
      echo "新记录插入成功";
      echo "\r\n";
      return $row;    
    }catch(PDOException $e){
      $GLOBALS['conn']->rollback();
      echo $e->getMessage();
      return '插入失败！';
}
    ```
