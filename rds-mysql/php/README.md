# 函数计算访问数据库 PHP 示例工程

示例包括如下两部分内容：

1. 在 initializer 中创建数据表

    ```php
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
    ```

2. 在 handler 中插入并查询数据

    ```php
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
    ```
