using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;

namespace HIMS.Core
{
    public static class CoreCommon
    {
        public static DateTime ToLocalDateTime(this DateTime OnDate, string Timezone)
        {
            TimeSpan ts = string.IsNullOrEmpty(Timezone) ? new TimeSpan() : TimeSpan.Parse(Timezone.Replace("+", ""));
            return OnDate.Add(ts);
        }
        public static DateTime ToUtcDateTime(this DateTime OnDate, string Timezone)
        {
            TimeSpan ts = string.IsNullOrEmpty(Timezone) ? new TimeSpan() : TimeSpan.Parse(Timezone.Replace("+", ""));
            return OnDate.Subtract(ts);
        }

        public static string GetValue(this List<SearchGrid> filters, string key)
        {
            return filters.Find(x => x.FieldName == key)?.FieldValue ?? "";
        }
    }
    public enum PagePermission
    {
        Add = 1, Edit = 2, Delete = 3, View = 4
    }
}
