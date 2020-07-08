using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDemo.Models
{
    public class ApiBaseModel
    {
        public bool Status { get; set; }
        public string Msg { get; set; }
        public object Result { get; set; }
        public int Total { get; set; }
    }
}
