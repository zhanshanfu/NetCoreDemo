using CSRedis;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreDemo.Tools.Redis
{
    public class RedisContext
    {
        public RedisContext(string connection)
        {
            Redis = new CSRedisClient(connection); ;

            RedisHelper.Initialization(Redis);
        }
        private CSRedisClient Redis { get; set; }
    }
}
