<?php
function initializer($context)
{
    $pdo = null;
    try {
        $pdo = new_pdo($context);
        $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

        $sql = "CREATE TABLE IF NOT EXISTS users (
            id      VARCHAR(64) NOT NULL,
            name    VARCHAR(128) NOT NULL,
            PRIMARY KEY(id))";
        $pdo->beginTransaction();
        $pdo->exec($sql);
        $pdo->commit();
    } catch (PDOException $e) {
        if (!empty($pdo)) {
            $pdo->rollback();
        }
        echo $e->getMessage();
    }
    return "The table `users` has been created!\r\n";
}

function handler($event, $context)
{
    $pdo = null;
    try {
        $pdo = new_pdo($context);
        $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

        $pdo->beginTransaction();
        $pdo->exec("REPLACE INTO users (`id`,`name`) values('4','too')");
        $pdo->commit();

        $stmt = $pdo->query("SELECT * FROM users");

        $users = '';
        while ($row = $stmt->fetch()) {
            $users = $users . ", " . $row['name'];
        }

        return $users;
    } catch (PDOException $e) {
        if (!empty($pdo)) {
            $pdo->rollback();
        }
        return $e->getMessage();
    }
}

function new_pdo($context)
{
    $mysql_host = $_ENV['MYSQL_HOST'];
    $mysql_port = $_ENV['MYSQL_PORT'];
    $mysql_user = $_ENV['MYSQL_USER'];
    $mysql_password = $_ENV['MYSQL_PASSWORD'];
    $mysql_dbname = $_ENV['MYSQL_DBNAME'];
    return new PDO("mysql:host=$mysql_host;port=$mysql_port;dbname=$mysql_dbname", $mysql_user, $mysql_password);
}
