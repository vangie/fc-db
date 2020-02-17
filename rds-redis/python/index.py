# -*- coding: utf-8 -*-
import os
import redis

def handler(event, context):
    host = os.environ['REDIS_HOST']
    port = os.environ['REDIS_PORT']
    password = os.environ['REDIS_PASSWORD']
    conn_pool = redis.ConnectionPool(host=host, password=password, port=port, decode_responses=True)
    r = redis.Redis(connection_pool=conn_pool)

    counter = r.get('counter')

    if counter is None:
        counter = 0
    else:
        counter = int(counter)

    print('counter: ' + str(counter))

    r.set('counter', str(counter + 1))
    return counter