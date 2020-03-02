STACK_NAME ?= mongo-test

start_mongo:
	bin/start_mongo.sh

stop_mongo:
	docker stop mongo_for_fc

clean: 
	docker rm -f mongo_for_fc

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