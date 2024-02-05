using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Times
{
    public static class DateTImeExtensions
    {
        public static DateTime ToDateTime(this string dateString, string format) {
            DateTime dateTime = DateTime.MinValue;
            try
            {
                dateTime = DateTime.ParseExact(dateString, format,
                                CultureInfo.InvariantCulture);
            }
            catch { 
                
            }

            return dateTime;
            
        }
    }
}
