using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreDemo.Tools
{
    /// <summary>
    /// 读取配置文件信息
    /// </summary>
    public class ConfigExtensions
    {
        private readonly IConfiguration configuration;
        public ConfigExtensions(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetAppsettings(string key)
        {
            return configuration.GetSection(key).Value;
        }
    }
}
