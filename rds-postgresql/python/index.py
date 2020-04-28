# -*- coding: utf-8 -*-
import logging
import psycopg2
import os,sys
logger = logging.getLogger()

def getConnection():
  try:
    conn = psycopg2.connect(
      database = os.environ['DATABASE'],
      user = os.environ['USER'],
      password = os.environ['PASSWORD'],
      host = os.environ['HOST'],
      port = os.environ['PORT'],
      )
    return conn
  except Exception as e:
    logger.error(e)
    logger.error("ERROR: Unexpected error: Could not connect to PostgreSQL instance.")
    sys.exit()

def conditionallyCreateUsersTable():
    conn = getConnection()
    cur = conn.cursor()
    cur.execute('''CREATE TABLE COMPANY
        (ID INT PRIMARY KEY     NOT NULL,
        NAME           TEXT    NOT NULL,
        AGE            INT     NOT NULL,
        ADDRESS        CHAR(50),
        SALARY         REAL);''')
    conn.commit()
    conn.close()

def initializer(context):
  conditionallyCreateUsersTable()

def handler(event, context):
  try:
    conn = getConnection()
    cur = conn.cursor()
    cur.execute("INSERT INTO COMPANY (ID,NAME,AGE,ADDRESS,SALARY) \
      VALUES (1, 'Paul', 32, 'California', 20000.00 )");
    conn.commit()
    return 'successfully'
  finally:
    conn.close()