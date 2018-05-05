<?php

require 'vendor/autoload.php';

const CHANNEL_NAME = "ASP_CORE_CHAN";

$client = new Predis\Client([
    'scheme' => 'tcp',
    'host' => 'localhost',
    'port' => 6379,
    'database' => 0,
    'read_write_timeout' => 0,
]);
$adapter = new \Superbalist\PubSub\Redis\RedisPubSubAdapter($client);
$adapter->subscribe(CHANNEL_NAME, function ($message) {
    var_dump($message);
});