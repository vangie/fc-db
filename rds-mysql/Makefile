
OSS_BUCKET ?= fun-pkg
STACK_NAME ?= mysql-test

start_mysql:
	bin/start_mysql.sh

stop_mysql:
	docker stop mysql_for_fc

build:
	IGNORE_TPL_VALIDATION=true fun build


deploy:
	fun package --oss-bucket $(OSS_BUCKET)
	fun deploy --use-ros --stack-name $(STACK_NAME)

invoke_java:
	IGNORE_TPL_VALIDATION=true fun invoke java

invoke_nodejs:
	IGNORE_TPL_VALIDATION=true fun invoke nodejs 

invoke_python:
	IGNORE_TPL_VALIDATION=true fun invoke nodejs

clean: 
	docker rm -f mysql_for_fc