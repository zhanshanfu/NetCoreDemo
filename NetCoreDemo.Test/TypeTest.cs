using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreDemo.DB.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace NetCoreDemo.Test
{
    public class TypeTest
    {

        [Fact(DisplayName = "Type²âÊÔ")]
        public void ObjGetTypes()
        {
            var type = typeof(WorkOrgList);
            var propertys = type.GetRuntimeFields().ToList();
            var str = "";
            propertys.Where(f => !f.Name.Equals("org_name") && !f.Name.Equals("org_id")).ToList().Where(f => !f.Equals("org_name") && !f.Equals("org_id")).ToList().ForEach(p =>
            {
                str += $" public decimal {p.Name};\r\n";
                str += $" public int? {p.Name}_RD;\r\n";
                str += $" public int? {p.Name}_RA;\r\n";
            });
            Debugger.Break();
        }
        public class WorkOrgList
        {
            public string org_name;
            public string org_id;
            public decimal phone_plateful;
            public decimal expl;
            public decimal houdel_call_succeed;
            public decimal mainbuild_vr;
            public decimal add_roel_per;
            public decimal look_cust;
            public decimal cust_new;
            public decimal custreview_look;
            public decimal custreview_mgr;
            public decimal invalidhousedel;
            public decimal recom;
            public decimal centa_expl_list_s;
            public decimal centa_expl_list_r;
            public decimal centa_expl_list;
            public decimal houdel_vr_s;
            public decimal houdel_vr_r;
            public decimal houdel_vr;
            public decimal website;
            public decimal is_online;
            public decimal store_num;
            public decimal sess;
            public decimal sess_0_1min;
            public decimal sess_1_3min;
            public decimal sess_1h;
            public decimal call_succ;
            public decimal plan_look;
            public DateTime rpt_date;
        }
    }
}