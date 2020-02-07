
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

invoke_dotnetcore:
	IGNORE_TPL_VALIDATION=true fun invoke dotnetcore 

invoke_python:
	IGNORE_TPL_VALIDATION=true fun invoke nodejs

invoke_php:
	IGNORE_TPL_VALIDATION=true fun invoke php

local_invoke_php:
	IGNORE_TPL_VALIDATION=true fun local invoke -t template.yml -t template.override.yml php

clean: 
	docker rm -f mysql_for_fc