using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Utilities
{
    public static class Utils
    {
        //public static JObject ParseJsonRequest(HttpContext context)
        //{
        //    int len = context.Request.ContentLength;
        //    byte[] b = context.Request.BinaryRead(len);
        //    String str = Encoding.UTF8.GetString(b);
        //    JObject o = JsonConvert.DeserializeObject(str) as JObject;

        //    if (o == null)
        //    {
        //        context.Response.StatusCode = 400;
        //        context.Response.End();
        //    }

        //    return o;
        //}

        public static List<Dictionary<string, object>> ToList(DataTable table)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            List<string> columns = new List<string>();
            foreach (DataColumn col in table.Columns) columns.Add(col.ColumnName);

            foreach (DataRow row in table.Rows)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                list.Add(d);
                foreach (string col in columns)
                {
                    d[col] = row[col];
                }
            }

            return list;
        }

        public static Dictionary<string, object> ToDictionary(this object entity)
        {
            return entity.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(entity, null));
        }

        //datetime
        public static string ToShortDateString(this DateTime E_SystemDate)
        {
            return E_SystemDate.ToString("MM/dd/yyyy");
        }

        //public static String To_HMSDateFormat(this object E_SystemDate)
        //{
        //    var dateToConvert = E_SystemDate.ToString(); //"Wed Oct 02 2013 00:00:00 GMT+0100 (GMT Daylight Time)";

        //    var format = "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz '(GMT Daylight Time)'";

        //    var date = DateTime.ParseExact(dateToConvert, format, CultureInfo.InvariantCulture);

        //    return date;
        //}

        //public static String To_HMSDateTimeFormat(this object E_SystemDate)
        //{
        //    //return E_SystemDate.toString("dd-MM-yyyy HH:mm:ss");
        //}

        public static string GetColValue(this DataTable dt, string colName, int row = 0)
        {
            if (dt.Rows.Count <= row)
                return "";
            try
            {
                return ConvertToString(dt.Rows[row][colName]);
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string ConvertToString(this object str)
        {
            if (str == DBNull.Value || (str ?? "") == "")
            {
                return "";
            }

            try
            {
                return Convert.ToString(str);
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static double ConvertToDouble(this object str)
        {
            if (str == DBNull.Value || (str ?? "") == "")
            {
                return 0;
            }

            try
            {
                return Convert.ToDouble(str);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static string To2DecimalPlace(this double str)
        {
            if (str <= 0)
            {
                return "0";
            }

            try
            {
                return str.ToString("#.##");
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public static string GetDateColValue(this DataTable dt, string colName, int row = 0, string format = "dd/MM/yyyy hh:mm tt")
        {
            if (dt.Rows.Count <= row)
                return "";
            try
            {
                return ConvertToDateString(dt.Rows[row][colName], format);
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string ConvertToDateString(this object str, string ouputFormat = "dd/MM/yyyy hh:mm tt")
        {
            if (str == DBNull.Value || (str ?? "") == "")
            {
                return "";
            }

            try
            {
                return Convert.ToDateTime(str).ToString(ouputFormat);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}