using NetCoreDemo.DB.Models;
using NetCoreDemo.Entity;
using NetCoreDemo.Tools;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using NetCoreDemo.Tools.Redis;
using System;
using System.Threading;

namespace NetCoreDemo.Service
{
    public class RedisService
    {
        private readonly static object _lock = new object();
        private readonly RedisContext redisContext;
        public RedisService(ConfigExtensions configExtensions,
            RedisContext redisContext)
        {
            this.redisContext = redisContext;
        }

        public bool Buy(string ip)
        {
            //lock (_lock)
            //{
            //var count = RedisHelper.Get<int>("apple");
            //RedisHelper.Set("time", DateTime.Now,1);
            //var count = RedisHelper.RPop<string>("cachetest");
            //if (string.IsNullOrEmpty(count))
            //{
            //    Console.WriteLine(count);
            //    Console.WriteLine($"抢购失败");
            //    return false;
            //}
            ////RedisHelper.Set("apple", --count);

            //Thread.Sleep(500);

            //Console.WriteLine($"抢购成功，剩余数量：{count}");
            RedisHelper.HSet("person", "name", "张三");
            RedisHelper.HSet("person", "sex", "男");
            RedisHelper.HSet("person", "age", "28");
            RedisHelper.HSet("person", "adress", "wuhan");
            Console.WriteLine($"person这个哈希中的age为:{RedisHelper.HGet<int>("person", "age")}");
            return true;
            //}

        }
    }

}
