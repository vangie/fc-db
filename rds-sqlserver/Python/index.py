# -*- coding: utf-8 -*-
import pymssql
def handler(event, context):
    
    conn = pymssql.connect(host='rm-bp102b430827dg2dv.sqlserver.rds.aliyuncs.com',
                                user='txd123',
                                password='Txd1231512315',
                                database='TestDB',
                                charset='utf8')
    cursor = conn.cursor()
    cursor.execute('SELECT * FROM inventory WHERE quantity > 152')
    
    result = ''

    for row in cursor:
        result += 'row = %r\n' % (row,)

    conn.close()
    return result