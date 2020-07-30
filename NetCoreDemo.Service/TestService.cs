using NetCoreDemo.DB.Models;
using NetCoreDemo.Entity;
using NetCoreDemo.Tools;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;

namespace NetCoreDemo.Service
{
    public class TestService
    {
        private readonly testContext testContext;
        private readonly ConfigExtensions configExtensions;
        public TestService(testContext testContext, ConfigExtensions configExtensions)
        {
            this.testContext = testContext;
            this.configExtensions = configExtensions;
        }
        public async void GetData()
        {
            using (HttpClient http = new HttpClient())
            {
                for (int i = 1; i <= 172; i++)
                {
                    var res = await http.GetAsync($"https://www.shanxi0357.cn/app/index.php?i=124&t=0&v=1.2.19&from=wxapp&c=entry&a=wxapp&do=getLotteryAllList&m=ly_caip&sign=98ce5c18469013c502bfd4849a0212c0&cid=1&page={i}");
                    var resStr = await res.Content.ReadAsStringAsync();
                    var appBall = JsonConvert.DeserializeObject<AppBall>(resStr);
                    var balls = appBall.data.Select(d => new Ball
                    {
                        Code = d.code,
                        Date = d.date,
                        R1 = d.red[0],
                        R2 = d.red[1],
                        R3 = d.red[2],
                        R4 = d.red[3],
                        R5 = d.red[4],
                        R6 = d.red[5],
                        B1 = d.blue[0]
                    });
                    testContext.Ball.AddRange(balls);
                }
                testContext.SaveChanges();
            };
        }

        public dynamic Test()
        {
            // var testConfig = configExtensions.GetAppsettings("TestConfig");
            var student = configExtensions.GetConfig<Student>();
            //return testContext.Ball.OrderByDescending(b => b.Date).Take(10);
            return student;
        }
    }
    public class Student
    {
        public string Name { get; set; }
        public int Sex { get; set; }
    }
}
