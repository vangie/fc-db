# -*- coding: utf-8 -*-
import logging
import psycopg2
import os,sys

logger = logging.getLogger()

def getConnection():
  try:
    conn = pymysql.connect(
      host = os.environ['MYSQL_HOST'],
      port = int(os.environ['MYSQL_PORT']),
      user = os.environ['MYSQL_USER'],
      passwd = os.environ['MYSQL_PASSWORD'],
      db = os.environ['MYSQL_DBNAME'],
      connect_timeout = 5)
    return conn
  except Exception as e:
    logger.error(e)
    logger.error("ERROR: Unexpected error: Could not connect to MySql instance.")
    sys.exit()

def conditionallyCreateUsersTable():
  try:
    conn = getConnection()
    with conn.cursor() as cursor:
      sql = """CREATE TABLE IF NOT EXISTS users (
        id        VARCHAR(64) NOT NULL,
        name    VARCHAR(128) NOT NULL,
        PRIMARY KEY(id))"""
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
      sql = "REPLACE INTO users (id, name) VALUES(%s, %s)"
      cursor.execute(sql, ('2', 'wan'))
      cursor.execute("SELECT * FROM users")
      result = cursor.fetchone()
      logger.info(result)
      return result
  finally:
    conn.close()