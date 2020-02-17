package example;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import com.aliyun.fc.runtime.Context;
import com.aliyun.fc.runtime.StreamRequestHandler;
import org.bson.BsonDocument;
import org.bson.BsonString;
import org.bson.Document;
import com.mongodb.MongoClient;
import com.mongodb.MongoClientOptions;
import com.mongodb.MongoClientURI;
import com.mongodb.MongoCredential;
import com.mongodb.ServerAddress;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoCursor;
import com.mongodb.client.MongoDatabase;

public class App implements StreamRequestHandler {

    private static String dbName = System.getenv("MONGO_DATABASE");
    private static String url = System.getenv("MONGO_URL");

    @Override
    public void handleRequest(InputStream inputStream, OutputStream outputStream, Context context) throws IOException {

        MongoClient mongoClient = new MongoClient(new MongoClientURI(url));

        try {
            MongoDatabase database = mongoClient.getDatabase(dbName);
            MongoCollection<Document> collection = database.getCollection("fc_col");
            Document doc = new Document();
            String demoName = "FC";
            doc.append("DEMO", "FC");
            doc.append("MSG", "Hello FunctionCompute For MongoDB");
            collection.insertOne(doc);
            System.out.println("insert document: " + doc);

            BsonDocument filter = new BsonDocument();
            filter.append("DEMO", new BsonString(demoName));
            MongoCursor<Document> cursor = collection.find(filter).iterator();

            while (cursor.hasNext()) {
                Document document = cursor.next();
                System.out.println("find document: " + document);
                outputStream.write(document.toString().getBytes());
            }
        } finally {
            mongoClient.close();
        }
    }

}
