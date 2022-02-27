using System;

namespace BLL.Calendar
{
    public enum Quarter
    {
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4
    }

    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    /// <summary>
    /// Common DateTime Methods.
    /// </summary>
    /// 
    public static class CalendarUtils
    {
        #region Quarters

        public static DateTime GetStartOfQuarter(int year, Quarter qtr)
        {
            return qtr switch
            {
                // 1st Quarter = January 1 to March 31
                Quarter.First => new DateTime(year, 1, 1, 0, 0, 0, 0),
                // 2nd Quarter = April 1 to June 30
                Quarter.Second => new DateTime(year, 4, 1, 0, 0, 0, 0),
                // 3rd Quarter = July 1 to September 30
                Quarter.Third => new DateTime(year, 7, 1, 0, 0, 0, 0),
                _ => new DateTime(year, 10, 1, 0, 0, 0, 0)
            };
        }

        public static DateTime GetEndOfQuarter(int year, Quarter qtr)
        {
            return qtr switch
            {
                // 1st Quarter = January 1 to March 31
                Quarter.First => new DateTime(year, 3, DateTime.DaysInMonth(year, 3), 23, 59, 59, 999),
                // 2nd Quarter = April 1 to June 30
                Quarter.Second => new DateTime(year, 6, DateTime.DaysInMonth(year, 6), 23, 59, 59, 999),
                // 3rd Quarter = July 1 to September 30
                Quarter.Third => new DateTime(year, 9, DateTime.DaysInMonth(year, 9), 23, 59, 59, 999),
                _ => new DateTime(year, 12, DateTime.DaysInMonth(year, 12), 23, 59, 59, 999)
            };
        }

        public static Quarter GetQuarter(Month month)
        {
            return month switch
            {
                // 1st Quarter = January 1 to March 31
                <= Month.March => Quarter.First,
                // 2nd Quarter = April 1 to June 30
                >= Month.April and <= Month.June => Quarter.Second,
                // 3rd Quarter = July 1 to September 30
                >= Month.July and <= Month.September => Quarter.Third,
                _ => Quarter.Fourth
            };
        }

        public static DateTime GetEndOfLastQuarter()
        {
            if ((Month) DateTime.Now.Month <= Month.March)
                //go to last quarter of previous year
                return GetEndOfQuarter(DateTime.Now.Year - 1, Quarter.Fourth);
            else //return last quarter of current year
                return GetEndOfQuarter(DateTime.Now.Year,
                    GetQuarter((Month) DateTime.Now.Month));
        }

        public static DateTime GetStartOfLastQuarter()
        {
            if ((Month) DateTime.Now.Month <= Month.March)
                //go to last quarter of previous year
                return GetStartOfQuarter(DateTime.Now.Year - 1, Quarter.Fourth);
            else //return last quarter of current year
                return GetStartOfQuarter(DateTime.Now.Year,
                    GetQuarter((Month) DateTime.Now.Month));
        }

        public static DateTime GetStartOfCurrentQuarter()
        {
            return GetStartOfQuarter(DateTime.Now.Year,
                GetQuarter((Month) DateTime.Now.Month));
        }

        public static DateTime GetEndOfCurrentQuarter()
        {
            return GetEndOfQuarter(DateTime.Now.Year,
                GetQuarter((Month) DateTime.Now.Month));
        }

        #endregion

        #region Weeks

        public static DateTime GetStartOfLastWeek()
        {
            int daysToSubtract = (int) DateTime.Now.DayOfWeek + 7;
            DateTime dt =
                DateTime.Now.Subtract(System.TimeSpan.FromDays(daysToSubtract));
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfLastWeek()
        {
            DateTime dt = GetStartOfLastWeek().AddDays(6);
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }

        public static DateTime GetStartOfCurrentWeek()
        {
            int daysToSubtract = (int) DateTime.Now.DayOfWeek;
            DateTime dt =
                DateTime.Now.Subtract(System.TimeSpan.FromDays(daysToSubtract));
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfCurrentWeek()
        {
            DateTime dt = GetStartOfCurrentWeek().AddDays(6);
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }

        #endregion

        #region Months

        public static DateTime GetStartOfMonth(Month month, int year)
        {
            return new DateTime(year, (int) month, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfMonth(Month month, int year)
        {
            return new DateTime(year, (int) month,
                DateTime.DaysInMonth(year, (int) month), 23, 59, 59, 999);
        }

        public static DateTime GetStartOfLastMonth()
        {
            if (DateTime.Now.Month == 1)
                return GetStartOfMonth((Month) 12, DateTime.Now.Year - 1);
            else
                return GetStartOfMonth((Month) (DateTime.Now.Month - 1), DateTime.Now.Year);
        }

        public static DateTime GetEndOfLastMonth()
        {
            if (DateTime.Now.Month == 1)
                return GetEndOfMonth((Month) 12, DateTime.Now.Year - 1);
            else
                return GetEndOfMonth((Month) (DateTime.Now.Month - 1), DateTime.Now.Year);
        }

        public static DateTime GetStartOfCurrentMonth()
        {
            return GetStartOfMonth((Month) DateTime.Now.Month, DateTime.Now.Year);
        }

        public static DateTime GetEndOfCurrentMonth()
        {
            return GetEndOfMonth((Month) DateTime.Now.Month, DateTime.Now.Year);
        }

        #endregion

        #region Years

        public static DateTime GetStartOfYear(int year)
        {
            return new DateTime(year, 1, 1, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfYear(int year)
        {
            return new DateTime(year, 12,
                DateTime.DaysInMonth(year, 12), 23, 59, 59, 999);
        }

        public static DateTime GetStartOfLastYear()
        {
            return GetStartOfYear(DateTime.Now.Year - 1);
        }

        public static DateTime GetEndOfLastYear()
        {
            return GetEndOfYear(DateTime.Now.Year - 1);
        }

        public static DateTime GetStartOfCurrentYear()
        {
            return GetStartOfYear(DateTime.Now.Year);
        }

        public static DateTime GetEndOfCurrentYear()
        {
            return GetEndOfYear(DateTime.Now.Year);
        }

        #endregion

        #region Days

        public static DateTime GetStartOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month,
                date.Day, 23, 59, 59, 999);
        }

        #endregion
    }
}