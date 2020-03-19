# -*- coding: utf-8 -*-
import logging, pymssql, os

logger = logging.getLogger()

def getConnection():
  try:
    conn = pymssql.connect(
      host = os.environ['MSSQL_SERVER'],
      port = int(os.environ['MSSQL_PORT']),
      user = os.environ['MSSQL_USER'],
      password = os.environ['MSSQL_PASSWORD'],
      database = os.environ['MSSQL_DATABASE'],
      charset = 'utf8')
    return conn
  except Exception as e:
    logger.error(e)
    logger.error("ERROR: Unexpected error: Could not connect to SQL Server instance.")
    sys.exit()

def conditionallyCreateUsersTable():
  try:
    conn = getConnection()
    with conn.cursor() as cursor:
      sql = """if not exists(select * from sysobjects where name='users' and xtype='U')
        create table users (
            id varchar(64) not null,
            name varchar(128) not null,
            PRIMARY KEY(id)
        )"""
      cursor.execute(sql)
    conn.commit()
  finally:
    conn.close()

def initializer(context):
  conditionallyCreateUsersTable()

def handler(event, context):
  try:
    conn = getConnection()
    with conn.cursor() as cursor:
      sql = """merge users as target
                using (values(%s)) 
                    as source(name)
                    on target.id = %s
                when matched then
                    update
                    set name = source.name
                when not matched then
                    insert (id, name)
                    values( %s, source.name);
      """
      cursor.execute(sql, ('wan', '2', '2'))
      cursor.execute("SELECT * FROM users")
      result = cursor.fetchone()
      logger.info(result)
      return result
  finally:
    conn.close()