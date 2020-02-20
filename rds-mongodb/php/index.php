<?php

require 'vendor/autoload.php';
function handler($event, $context)
{

    $db_name = $_ENV['MONGO_DATABASE'];
    $uri = $_ENV['MONGO_URL'];

    $client = new MongoDB\Client($uri);

    $collection = $client->$db_name->fc_col;
    
    $result = $collection->insertOne(['DEMO' => 'FC', 'MSG' => 'Hello FunctionCompute For MongoDB']);
    echo "Inserted with Object ID '{$result->getInsertedId()}'", "\n";
    $cursor = $collection->find(['DEMO' => 'FC']);
    $result = '';
    foreach ($cursor as $entry) {
        echo json_encode($entry->getArrayCopy()), "\n";
        $result = $result . json_encode($entry->getArrayCopy()) . "\n";
    }
    return $result;
}