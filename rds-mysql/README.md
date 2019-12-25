# 函数计算访问 MySQL 示例集

## 依赖工具

本项目是在 MacOS 下开发的，涉及到的工具是平台无关的，对于 Linux 和 Windows 桌面系统应该也同样适用。在开始本例之前请确保如下工具已经正确的安装，更新到最新版本，并进行正确的配置。

* [Docker](https://www.docker.com/)
* [Funcraft](https://github.com/aliyun/fun)

Fun 工具依赖于 docker 来模拟本地环境。

对于 MacOS 用户可以使用 [homebrew](https://brew.sh/) 进行安装：

```bash
brew cask install docker
brew tap vangie/formula
brew install fun
```

Windows 和 Linux 用户安装请参考：

1. https://github.com/aliyun/fun/blob/master/docs/usage/installation.md

安装好后，记得先执行 `fun config` 初始化一下配置。

**注意**, 如果你已经安装过了 funcraft，确保 funcraft 的版本在 3.0.6 以上。

```bash
$ fun --version
3.0.6
```

## 初始化

```bash
git clone http://github.com/vangie/fc-db
cd fc-db/rds-mysql
```



## 构建

```bash
make build
```

## 部署

新建或者找一个已有的 OSS Bucket ，其所属的 region 与 fun config 时指定的 region 相同，用于存放 fun package 打包的代码包。将新建的 Bucket 名称替换下面命令的 `xxx` 部分。

```bash
OSS_BUCKET=xxx make deploy
```

## 测试

```bash
# java
make invoke_java

# nodejs
make invoke_nodejs

# python
make invoke_python

```

## 示例工程源码

* [Nodejs 示例工程](nodejs)
* [Python 示例工程](python)
* [Java 示例工程](java)
