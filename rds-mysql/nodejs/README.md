# 函数计算访问数据库 Nodejs 示例工程

该项目采用了社区库 [serverless-mysql](https://github.com/jeremydaly/serverless-mysql)。该库包装了 mysql 库，并针对 serverless 场景做了如下改进：

1. 返回 Promise 以更好的支持异步请求
2. 使用指数退避来处理失败的连接
3. 监听活跃连接，并在活跃连接数达到当前 MYSQL 用户服务端连接数限制的某个比例时进行连接回收。
4. 支持事物

示例包括如下两部分内容：

1. 在 initializer 中创建数据表

    ```javascript
    async function conditionallyCreateUsersTable() {
    const createTableSQL = `CREATE TABLE IF NOT EXISTS users (
        id        VARCHAR(64) NOT NULL,
        name    VARCHAR(128) NOT NULL,
        PRIMARY KEY(id))`;
    return await mysql.transaction()
    .query(createTableSQL)
    .commit();
    }
    ```

2. 在 handler 中插入数据

    ```javascript
    let results = await mysql.transaction()
    .query('REPLACE INTO users (id, name) VALUES(?, ?)', ['1', 'vangie'])
    .commit();
    ```

## FAQ

1. nodejs 无法连接 mysql8 的问题，报错"ER_NOT_SUPPORTED_AUTH_MODE"

    该问题需要改变一下 mysql 用户的密码加密方式，示例的 SQL 语句如下

    ```SQL
    create user nodeuser@localhost identified by 'nodeuser@1234';
    grant all privileges on *.* to nodeuser@localhost;
    ALTER USER 'nodeuser'@localhost IDENTIFIED WITH mysql_native_password BY 'nodeuser@1234';

    create user nodeuser@'%' identified by 'nodeuser@1234';
    grant all privileges on *.* to nodeuser@'%';
    ALTER USER 'nodeuser'@'%' IDENTIFIED WITH mysql_native_password BY 'nodeuser@1234';
    ```

    https://waylau.com/node.js-mysql-client-does-not-support-authentication-protocol/