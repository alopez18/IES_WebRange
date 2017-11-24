using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.DataLayer {
    public class Utils {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp) {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }


        public static int DateTime2UnixTime(DateTime date) {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }


        //public static String ToJulianDate(DateTime date) {
        //    String res = string.Format("{0:yy}{1:D3}", date, date.DayOfYear);
        //    return res;
        //}

        //public static double ToJulianDate(DateTime date) {
        //    return date.ToOADate() + 2415018.5;
        //}

        //public static DateTime? FromJulianDate(String julianDate) {
        //    // TODO add validation logic, plus rules to cope with pre-2000 dates
        //    var yy = 1900 + int.Parse(julianDate.Substring(0, 2));
        //    int days = (int.Parse(julianDate) % 1000) - 1;

        //    //var ddd = int.Parse(julianDate.ToString().Substring(2));
        //    DateTime res = new DateTime(yy, 1, 1).AddDays(days);
        //    return res;
        //}

        //public static DateTime? FromJulianDate(double julianDate) {
        //    double unixTime = (julianDate - 2440587.5) * 86400;
        //    System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        //    dtDateTime = dtDateTime.AddSeconds(unixTime).ToLocalTime();
        //    return dtDateTime;

        //    //if (!String.IsNullOrWhiteSpace(julianDate)) {
        //    //    // TODO add validation logic, plus rules to cope with pre-2000 dates
        //    //    var yy = 2000 + int.Parse(julianDate.Substring(0, 2));
        //    //    var ddd = int.Parse(julianDate.Substring(2));
        //    //    DateTime res = new DateTime(yy, 1, 1).AddDays(ddd);
        //    //    return res;
        //    //} else {
        //    //    return null;
        //    //}
        //}



        public static string Convert_GregorianDate_To_JulianDate(DateTime gregorianDate) {
            string jdeDate = string.Empty;

            //DateTime parsedDate;

            //if (DateTime.TryParseExact(gregorianDate, "yyyyMMddHHmm", null, System.Globalization.DateTimeStyles.None, out parsedDate)) {
            jdeDate = (1000 * (gregorianDate.Year - 1900) + gregorianDate.DayOfYear).ToString();
            //}

            return jdeDate;
        }

        public static DateTime? Convert_JDEDate_To_GregorianDate(string jdeDate) {
            string day = string.Empty;
            string year = string.Empty;

            if (jdeDate.Length == 5) {
                // 5 character string for pre-year 2000 dates, year is first 2 chars
                year = jdeDate.Substring(0, 2);
            } else if (jdeDate.Length == 6) {
                // 6 characters for post 2000 dates, take first three characters
                year = jdeDate.Substring(0, 3);
            } else {
                return null;
            }

            // take last three characters for day
            day = jdeDate.Substring(jdeDate.Length - 3, 3);

            // calculate gregorian date
            DateTime gregorianDate = DateTime.Parse("1 Jan 1900");
            gregorianDate = gregorianDate.AddYears(Convert.ToInt16(year));
            gregorianDate = gregorianDate.AddDays(Convert.ToInt16(day) - 1);

            return gregorianDate;
        }


    }//Class Finish
}//Namespace Finish
