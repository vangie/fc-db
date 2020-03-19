<?php

function initializer($context)
{
    $pdo = null;
    try {
        $pdo = new_pdo($context);
        $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

        $sql = "if not exists(select * from sysobjects where name='users' and xtype='U')
            create table users (
                id varchar(64) not null,
                name varchar(128) not null,
                PRIMARY KEY(id)
            )";
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
        $pdo->exec("
            merge users as target
            using (values('too')) 
                as source(name)
                on target.id = '4'
            when matched then
                update
                set name = source.name
            when not matched then
                insert (id, name)
                values( '4', source.name);
        ");
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
    $mssql_server = $_ENV['MSSQL_SERVER'];
    $mssql_port = $_ENV['MSSQL_PORT'];
    $mssql_user = $_ENV['MSSQL_USER'];
    $mssql_password = $_ENV['MSSQL_PASSWORD'];
    $mssql_database = $_ENV['MSSQL_DATABASE'];
    return new PDO("sqlsrv:Server=$mssql_server, $mssql_port;Database=$mssql_database", $mssql_user, $mssql_password);
}



