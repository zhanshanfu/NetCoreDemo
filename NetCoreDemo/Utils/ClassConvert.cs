using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreDemo.Utils
{
    public static class ClassConvert
    {
        public static TChild ToChild<TParent, TChild>(this TParent parent) where TChild : TParent, new()
        {
            TChild child = new TChild();
            var ParentType = typeof(TParent);
            var Properties = ParentType.GetProperties();
            foreach (var Propertie in Properties)
            {
                //循环遍历属性
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    //进行属性拷贝
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }
            return child;
        }
        public static T1 PropertieCopy<T1>(this T1 t1,T1 t2)  
        {
            var ParentType = typeof(T1);
            var Properties = ParentType.GetProperties();
            foreach (var Propertie in Properties)
            {
                //循环遍历属性
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    //进行属性拷贝
                    Propertie.SetValue(t2, Propertie.GetValue(t1, null), null);
                }
            }
            return t1;
        }
        public static string ToWeekDay(this DateTime dateTime)
        {
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "星期日";

                case DayOfWeek.Monday:
                    return "星期一";

                case DayOfWeek.Saturday:
                    return "星期二";

                case DayOfWeek.Sunday:
                    return "星期三";

                case DayOfWeek.Thursday:
                    return "星期四";

                case DayOfWeek.Tuesday:
                    return "星期五";

                case DayOfWeek.Wednesday:
                    return "星期六";

                default:
                    return "";
            }

        }
        public static bool NotNullOrEmpty<T>(this List<T> list)
        {
            if (list != null && list.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
