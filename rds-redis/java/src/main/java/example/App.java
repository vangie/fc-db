package example;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import redis.clients.jedis.Jedis;
import com.aliyun.fc.runtime.Context;
import com.aliyun.fc.runtime.StreamRequestHandler;

public class App implements StreamRequestHandler {

    private String host = System.getenv("REDIS_HOST");
    private int port = Integer.parseInt(System.getenv("REDIS_PORT"));
    private String passwd = System.getenv("REDIS_PASSWORD");

    @Override
    public void handleRequest(InputStream inputStream, OutputStream outputStream, Context context) throws IOException {
        try {
            Jedis jedis = new Jedis(host, port);
            jedis.auth(passwd);

            String counter = jedis.get("counter");
            if (counter == null) {
                counter = "0";
            }

            System.out.println("counter: " + counter);

            jedis.set("counter", Integer.toString(Integer.parseInt(counter) + 1));
            jedis.quit();
            jedis.close();

            outputStream.write(counter.getBytes());
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
