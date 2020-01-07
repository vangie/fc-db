<?php
function initializer($context) {
    $servername = $_ENV['MYSQL_HOST'];
    $username = $_ENV['MYSQL_USER'];
    $password = $_ENV['MYSQL_PASSWORD'];
    $dbname = $_ENV['MYSQL_DBNAME'];
    global $conn;

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
}

function handler($event, $context) {
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

}