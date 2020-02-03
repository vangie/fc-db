STACK_NAME ?= redis-test

start_redis:
	bin/start_redis.sh

stop_redis:
	docker stop redis_for_fc

clean: 
	docker rm -f redis_for_fc

build:
	IGNORE_TPL_VALIDATION=true fun build

deploy:
	fun package
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