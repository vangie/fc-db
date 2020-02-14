<?php
function handler($event, $context)
{
    $host = $_ENV['REDIS_HOST'];
    $port = $_ENV['REDIS_PORT'];
    $pwd = $_ENV['REDIS_PASSWORD'];
    $redis = new Redis();
    if ($redis->connect($host, $port) == false) {
        die($redis->getLastError());
    }
    if ($redis->auth($pwd) == false) {
        die($redis->getLastError());
    }

    $counter = $redis->get("counter");

    if($counter == null) {
        $counter = 0;
    }

    echo "counter: $counter";

    if ($redis->set("counter", $counter + 1) == false) {
        die($redis->getLastError());
    }

    return $counter;
}
