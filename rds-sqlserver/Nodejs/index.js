'use strict';
const sql = require('mssql')

exports.handler = async function(event, context, callback) {
  
    const config = {
        user: 'txd123',
        password: 'Txd1231512315',
        server: 'rm-bp102b430827dg2dv.sqlserver.rds.aliyuncs.com', 
        database: 'TestDB',
    }

    try {
        let pool = await sql.connect(config)
        let result = await pool.request()
            .query('SELECT * FROM inventory WHERE quantity > 152')
            
        console.log(result)
    } catch (err) {
        console.log(err)
    }

    callback(null, 'hello world');
  };