# 函数计算访问数据库示例集

该示例集主要收集和整理函数计算访问数据库（MySQL、Redis 和 MangoDB等）的工程示例，可以借助于 ROS template.yml 快速地搭建出完整的云原生数据库示例应用（包括 FC 函数、VPC、VSwitch、SecurityGroup 和数据库实例）。其实每个示例的源码也是我们推荐的最佳实践，供大家参考。

**注意** 由于数据库实例会产生一些费用，所以运行之前请确定账号里有余额。当然为了节省用户的开支，我们选用了最小的按量实例，MySQL 数据库的费用是 ￥0.236/小时，体验完成以后，建议去 ROS 的控制台删除所有云资源实例。

* [MySQL 数据库](rds-mysql)
  * [Nodejs 示例工程](rds-mysql/nodejs)
  * [Python 示例工程](rds-mysql/python)
  * [Java 示例工程](rds-mysql/java)