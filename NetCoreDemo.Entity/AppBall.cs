using System;
using System.Collections.Generic;

namespace NetCoreDemo.Entity
{
    public class PrizegradesItem
    {
        /// <summary>
        /// 一等奖
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string typemoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string typenum { get; set; }
    }

    public class DataItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int c_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 双色球
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string date { get; set; }
        /// <summary>
        /// 二
        /// </summary>
        public string week { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> red { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> blue { get; set; }
        /// <summary>
        /// 3.57亿
        /// </summary>
        public string sales { get; set; }
        /// <summary>
        /// 8.37亿
        /// </summary>
        public string poolmoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PrizegradesItem> prizegrades { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int show { get; set; }
    }

    public class AppBall
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 数据执行成功
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalPage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DataItem> data { get; set; }
    }

}
