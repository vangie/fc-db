'use strict';
const sql = require('mssql');

const config = {
    user: process.env.MSSQL_USER,
    password: process.env.MSSQL_PASSWORD,
    server: process.env.MSSQL_SERVER,
    port: parseInt(process.env.MSSQL_PORT),
    database: process.env.MSSQL_DATABASE,
}

async function conditionallyCreateUsersTable() {

    try {
        let pool = await sql.connect(config)
        let result = await pool.request()
            .query(`
                if not exists(select * from sysobjects where name='users' and xtype='U')
                create table users (
                    id varchar(64) not null,
                    name varchar(128) not null,
                    PRIMARY KEY(id)
                )
            `);
        console.dir(result);
    } catch (err) {
        console.error(err);
    }
}

module.exports.initializer = async function (context, callback) {
    await conditionallyCreateUsersTable();
    callback(null, '');
};

exports.handler = async function (event, context, callback) {

    try {
        let pool = await sql.connect(config)
        let result = await pool.request()
            .input('id', 1)
            .input('name', 'vangie')
            .query(`
                merge users as target
                using (values(@name)) 
                    as source(name)
                    on target.id = @id
                when matched then
                    update
                    set name = source.name
                when not matched then
                    insert (id, name)
                    values( @id, source.name);
            `);

        callback(null, result);
    } catch (err) {
        console.error(err)
        callback(err, "insert failed!");
    }
};