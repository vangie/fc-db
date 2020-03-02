#!/bin/bash

CONTAINER_NAME=mongo_for_fc
USERNAME=mongoadmin
PASSWORD=secret

if [ ! "$(docker ps -a | grep $CONTAINER_NAME)" ]; then 
    docker run --name $CONTAINER_NAME \
    -e MONGO_INITDB_ROOT_USERNAME=$USERNAME \
    -e MONGO_INITDB_ROOT_PASSWORD=$PASSWORD \
    -p 27017:27017 \
    mongo 
else
    docker start $CONTAINER_NAME
fi