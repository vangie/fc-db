'use strict';

const pg = require('pg');
const pgConfig = {
    user: process.env.USER,           // 数据库用户名
    database: process.env.DATABASE,       // 数据库
    password: process.env.PASSWORD,       // 数据库密码
    host: process.env.HOST,        // 数据库所在IP
    port: process.env.PORT                // 连接端口
};
const pool = new pg.Pool(pgConfig);

async function conditionallyCreateUsersTable() {
  pool.connect(function(error, client, done) {
    let sqlStr = 'CREATE TABLE COMPANY01 (ID INT PRIMARY KEY  NOT NULL, NAME  TEXT  NOT NULL, AGE  INT  NOT NULL, ADDRESS  CHAR(50), SALARY  REAL)';
    client.query(sqlStr, [], function(err, response) {
      done();
      console.log(response.rows)
    })
  })
}


module.exports.initializer = async function(context, callback) {
  await conditionallyCreateUsersTable();
  callback(null, '');
};


module.exports.handler = async function(event, context, callback) {
  try {
    pool.connect(function(error, client, done) {
      let sqlStr = 'SELECT * FROM company';
      client.query(sqlStr, [], function(err, response) {
        done();
        console.log(response.rows)
        callback(null, response.rows);
      })
    })
  
  } catch (err) {
    console.error(err)
    callback(err, "Inquire failed!");
  }
}

initializer("ttt", ()=>{});
