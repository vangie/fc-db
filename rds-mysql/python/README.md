# 函数计算访问数据库 Python 示例工程

示例包括如下两部分内容：

1. 在 initializer 中创建数据表

    ```python
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
    ```

2. 在 handler 中插入并查询数据

    ```python
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
    ```
