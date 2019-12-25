#!/bin/bash

if [ ! "$(docker ps -a | grep mysql_for_fc)" ]; then 
    docker run --name mysql_for_fc \
    --env MYSQL_ROOT_PASSWORD=my-secret-pw \
    --env MYSQL_DATABASE=mydb \
    --env MYSQL_USER=fc-user \
    --env MYSQL_PASSWORD=my-secret-pw \
    --env TZ=Asia/Shanghai \
    -p 33061:3306 \
    mysql --default-authentication-plugin=mysql_native_password
else
    docker start mysql_for_fc
fi