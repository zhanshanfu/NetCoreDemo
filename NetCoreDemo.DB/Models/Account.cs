using System;
using System.Collections.Generic;

namespace NetCoreDemo.DB.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Sex { get; set; }
    }
}
