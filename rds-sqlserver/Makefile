STACK_NAME ?= sqlserver-test-1

start_sqlserver:
	bin/sqlserver.sh start

stop_sqlserver:
	bin/sqlserver.sh stop

connect_sqlserver:
	bin/sqlserver.sh connect

clean_sqlserver:
	bin/sqlserver.sh clean

build:
	IGNORE_TPL_VALIDATION=true fun build -d

deploy:
	fun package
	fun deploy --use-ros --stack-name $(STACK_NAME) -y

invoke_java:
	IGNORE_TPL_VALIDATION=true fun invoke java

invoke_nodejs:
	IGNORE_TPL_VALIDATION=true fun invoke nodejs

invoke_python:
	IGNORE_TPL_VALIDATION=true fun invoke python

invoke_php:
	IGNORE_TPL_VALIDATION=true fun invoke php

invoke_dotnetcore:
	IGNORE_TPL_VALIDATION=true fun invoke dotnetcore

local_invoke_java:
	IGNORE_TPL_VALIDATION=true fun local invoke java

local_invoke_nodejs:
	IGNORE_TPL_VALIDATION=true fun local invoke nodejs

local_invoke_python:
	IGNORE_TPL_VALIDATION=true fun local invoke python

local_invoke_php:
	IGNORE_TPL_VALIDATION=true fun local invoke php

local_invoke_dotnetcore:
	IGNORE_TPL_VALIDATION=true fun local invoke dotnetcore