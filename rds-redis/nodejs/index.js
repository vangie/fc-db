'use strict';
var redis = require('redis');

exports.handler = (event, context, callback) => {
  const host = process.env.REDIS_HOST;
  const port = process.env.REDIS_PORT;
  const passwd = process.env.REDIS_PASSWORD;

  const client = redis.createClient(port, host, {string_numbers: false});
  client.auth(passwd, redis.print);

  client.on("error", function (err) {
    console.error(err);
    callback(err, "Error");
  });

  client.get("counter", (err, counter) => {
    if(err){
      console.error(err);
      callback(err, "Error");
      return;
    }
    if(counter === null){
      counter = 0;
    }
    console.log('counter: ', counter);
    client.set('counter', parseInt(counter) + 1, (err) => {
      if(err){
        console.error(err);
        callback(err, "Error");
        return;
      }
      client.quit();
      callback(null, {counter})
    })
  })
};