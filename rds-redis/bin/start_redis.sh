#!/bin/bash

CONTAINER_NAME=redis_for_fc
REDIS_PASSWORD=foobared

if [ ! "$(docker ps -a | grep $CONTAINER_NAME)" ]; then 
    docker run --name $CONTAINER_NAME \
    -p 6379:6379 \
    redis redis-server --requirepass "$REDIS_PASSWORD"
else
    docker start $CONTAINER_NAME
fi