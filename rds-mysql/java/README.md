# 函数计算访问数据库 Java 示例工程

示例包括如下两部分内容：

1. 在 initializer 中创建数据表

    ```java
    String sql = "CREATE TABLE IF NOT EXISTS users (\n" +
            "      id        VARCHAR(64) NOT NULL,\n" +
            "      name    VARCHAR(128) NOT NULL,\n" +
            "      PRIMARY KEY(id))";

    try (Connection conn = getConnection()) {

        Statement stmt = conn.createStatement();
        stmt.executeUpdate(sql);

    } catch (SQLException e) {
        e.printStackTrace();
    }
    ```

2. 在 handler 中插入并查询数据

    ```java
    try (Connection conn = getConnection()) {

        String sql = "REPLACE INTO users (id, name) VALUES(?, ?)";

        PreparedStatement ps = conn.prepareStatement(sql);
        ps.setString(1, "3");
        ps.setString(2, "du");

        ps.execute();

        resultSet = stmt.executeQuery("SELECT * FROM users");

        if (resultSet.next()) {
            logger.info("user: " + resultSet.getString(2));
        }

    } catch (SQLException e) {
        e.printStackTrace();
    }
    ```
