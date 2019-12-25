'use strict';

const mysql = require('serverless-mysql')({
  config: {
    host      : process.env.MYSQL_HOST,
    port      : process.env.MYSQL_PORT,
    database  : process.env.MYSQL_DBNAME,
    user      : process.env.MYSQL_USER,
    password  : process.env.MYSQL_PASSWORD,
    insecureAuth : true
  }
});

async function conditionallyCreateUsersTable() {
  const createTableSQL = `CREATE TABLE IF NOT EXISTS users (
    id        VARCHAR(64) NOT NULL,
    name    VARCHAR(128) NOT NULL,
    PRIMARY KEY(id))`;
  return await mysql.transaction()
  .query(createTableSQL)
  .commit();
}


module.exports.initializer = async function(context, callback) {
  await conditionallyCreateUsersTable();
  callback(null, '');
};


module.exports.handler = async function(event, context, callback) {
  let results = await mysql.transaction()
  .query('REPLACE INTO users (id, name) VALUES(?, ?)', ['1', 'vangie'])
  .commit();

  callback(null, results);
}