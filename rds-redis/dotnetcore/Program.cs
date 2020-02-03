using System;
using System.IO;
using System.Text;
using Aliyun.Serverless.Core;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;

namespace redis
{
    public class RedisHandler
    {
        private static String host = Environment.GetEnvironmentVariable("REDIS_HOST");
        private static String port = Environment.GetEnvironmentVariable("REDIS_PORT");
        private static String passwd = Environment.GetEnvironmentVariable("REDIS_PASSWORD");
        private static String connStr = $"{host}:{port},password={passwd},connectTimeout=5000,writeBuffer=40960";

        public Stream Handler(Stream input, IFcContext context)
        {
            RedisCache cache = new RedisCache(new RedisCacheOptions
            {
                Configuration = connStr
            });

            var counterBytes = cache.Get("dotnet_counter");
            var counter = 0;
            if (counterBytes != null)
            {
                counter = BitConverter.ToInt32(counterBytes, 0);
            }
            Console.WriteLine($"Counter: {counter}");

            cache.Set("dotnet_counter", BitConverter.GetBytes(counter + 1));

            counterBytes = BitConverter.GetBytes(counter);
            MemoryStream output = new MemoryStream();
            output.Write(counterBytes, 0, counterBytes.Length);
            output.Seek(0, SeekOrigin.Begin);
            return output;
        }

    }
}
