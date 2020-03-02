# -*- coding: utf-8 -*-
import os
from pymongo import MongoClient

def handler(event, context):
    dbName = os.environ['MONGO_DATABASE']
    url = os.environ['MONGO_URL']

    client = MongoClient(url)

    col = client[dbName]['fc_col']
    col.insert_one(dict(DEMO="FC", MSG="Hello FunctionCompute For MongoDB"))
    doc=col.find_one(dict(DEMO="FC"))
    print ('find documents:'+ str(doc))
    return str(doc)