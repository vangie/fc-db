<?php


function handler($event, $context)
{
    try {
        $conn = new PDO("sqlsrv:Server=rm-bp102b430827dg2dv.sqlserver.rds.aliyuncs.com;Database=TestDB","txd123","Txd1231512315");
        // set the PDO error mode to exception
        $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        $conn->query("set names utf-8");
        $sql="SELECT * FROM inventory WHERE quantity > 152";
        $result = $conn->prepare($sql);
        $result->execute();
        print($result);
        return ("Connection successed.");
    } catch (PDOException $e) {
        return ("Connection failed: " . $e->getMessage());
    }
}



