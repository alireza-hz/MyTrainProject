using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Convert
{
    public static class Dataconvert
    {

        public static string Toshamsi(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(date) + "/" + pc.GetMonth(date).ToString("00") + "/" + pc.GetDayOfMonth(date).ToString("00");


        }

		public static string ToshamsiGetMounth(this DateTime date)
		{
			PersianCalendar pc = new PersianCalendar();
			return pc.GetYear(date) + "/" + pc.GetMonth(date).ToString("00");


		}
		public static string ToshamsiGetDetyear (this DateTime date)
		{
			PersianCalendar pc = new PersianCalendar();
			return pc.GetYear(date).ToString();

		}


	}
}