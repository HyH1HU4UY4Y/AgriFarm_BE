using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedApplication.Times.Services
{
    public class TimePickerHelper
    {

        public bool IsNotAfterDate(DateTime date)
        {
            return DateTime.Today > date;
        }
    }
}
