#!/bin/bash

CONTAINER_NAME=sqlserver_for_fc
SA_PASSWORD=Foobared!

case "$1" in
    "start")
        if [ ! "$(docker ps -a | grep $CONTAINER_NAME)" ]; then
            docker run --name $CONTAINER_NAME \
            -e 'ACCEPT_EULA=Y' \
            -e "SA_PASSWORD=$SA_PASSWORD" \
            -p 1433:1433 \
            mcr.microsoft.com/mssql/server:2017-latest-ubuntu
        else
            docker start $CONTAINER_NAME
        fi
    ;;
    "connect" | "conn")
        docker exec -it $CONTAINER_NAME /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD
    ;;
    "stop")
        docker stop $CONTAINER_NAME
    ;;
    "clean")
        docker stop $CONTAINER_NAME && docker rm $CONTAINER_NAME
    ;;
    *)
        echo "unknown argument";
        exit 1
    ;;
esac



