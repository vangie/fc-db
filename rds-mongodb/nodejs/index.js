'use strict';
const MongoClient = require('mongodb').MongoClient;
const assert = require('assert');

exports.handler = async (event, context, callback) => {

    const dbName = process.env.MONGO_DATABASE;
    const url = process.env.MONGO_URL;

    const client = new MongoClient(url);

    try {
        await client.connect();
        const db = client.db(dbName);

        const demoName = "FC";
        const doc = {
            "DEMO": demoName,
            "MSG": "Hello FunctionCompute For MongoDB"
        };

        let col = db.collection('fc_col');

        let r = await col.insertOne(doc);
        assert.equal(1, r.insertedCount);

        const filter = { "DEMO": demoName };
        let res = await col.findOne(filter);

        console.log(res);
        callback(null, res);

    } catch (err) {
        console.log(err.stack);
        callback(err, "Error");
    } finally {
        client.close();
    }
};