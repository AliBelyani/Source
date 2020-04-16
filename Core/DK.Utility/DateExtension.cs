using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DK.Utility
{
    public static class DateExtension
    {
        public static string TimeAgo(this DateTime _Date)
        {
            try
            {
                TimeSpan timeSince = DateTime.Now.Subtract(_Date);

                if (timeSince.TotalMilliseconds < 1)
                    return "هنوز نه";
                if (timeSince.TotalMinutes < 1)
                    return "همین الان";
                if (timeSince.TotalMinutes < 2)
                    return "یک دقیقه پیش";
                if (timeSince.TotalMinutes < 60)
                    return string.Format("{0} {1}", timeSince.Minutes, "دقیقه پیش");
                if (timeSince.TotalMinutes < 120)
                    return "یک ساعت پیش";
                if (timeSince.TotalHours < 24)
                    return string.Format("{0} {1}", timeSince.Hours, "ساعت پیش");
                if (timeSince.TotalDays == 1)
                    return "دیروز";
                if (timeSince.TotalDays < 7)
                    return string.Format("{0} {1}", timeSince.Days, "روز پیش");
                if (timeSince.TotalDays < 14)
                    return "هفته پیش";
                if (timeSince.TotalDays < 21)
                    return " 2 هفته پیش";
                if (timeSince.TotalDays < 28)
                    return "3 هفته پیش";
                if (timeSince.TotalDays < 60)
                    return "یک ماه پیش";
                if (timeSince.TotalDays < 365)
                    return string.Format("{0} {1}", Math.Round(timeSince.TotalDays / 30), "ماه پیش");
                if (timeSince.TotalDays < 730)
                    return "سال قبل";

                //last but not least...
                return string.Format("{0} {1}", Math.Round(timeSince.TotalDays / 365), "سال قبل");
            }
            catch (Exception ex)
            {
                //Logging.LogError(ex);
                return "";
            }
        }
        public static string ConvertToPersian(DateTime _DateTime, string _Format = "{Year}/{Month}/{Day}")
        {
            try
            {
                PersianCalendar _PersianCalendar = new PersianCalendar();
                string _PersianDate = null;
                _PersianDate = _Format.ToLower().Replace("{Year}".ToLower(), _PersianCalendar.GetYear(_DateTime).ToString("0000"))
                                .Replace("{Month}".ToLower(), _PersianCalendar.GetMonth(_DateTime).ToString("00"))
                                .Replace("{Day}".ToLower(), _PersianCalendar.GetDayOfMonth(_DateTime).ToString("00"));
                return _PersianDate;
            }
            catch (Exception ex)
            {
                //Logging.LogError(ex);
                return "";
            }
        }
        public static string ConvertToPersian(object _DateTime, string _Format = "{Year}/{Month}/{Day}")
        {
            try
            {
                if (_DateTime == null || string.IsNullOrWhiteSpace(_DateTime.ToString()))
                    return "";
                return ConvertToPersian(Convert.ToDateTime(_DateTime), _Format);
            }
            catch (Exception ex)
            {
                //Logging.LogError(ex);
                return "";
            }
        }
        public static string ToPersian(this DateTime _DateTime, string _Format = "{Year}/{Month}/{Day}")
        {
            try
            {
                PersianCalendar _PersianCalendar = new PersianCalendar();
                string _PersianDate = null;
                _PersianDate = _Format.ToLower().Replace("{Year}".ToLower(), _PersianCalendar.GetYear(_DateTime).ToString("0000"))
                                .Replace("{Month}".ToLower(), _PersianCalendar.GetMonth(_DateTime).ToString("00"))
                                .Replace("{Day}".ToLower(), _PersianCalendar.GetDayOfMonth(_DateTime).ToString("00"));
                return _PersianDate;
            }
            catch (Exception ex)
            {
                //Logging.LogError(ex);
                return "";
            }
        }
        public static DateTime ConvertToGregorian(string _DateTime)
        {
            try
            {
                _DateTime = _DateTime.Trim();
                PersianCalendar _PersianCalendar = new PersianCalendar();
                int xYear = _DateTime.Substring(0, 4).ToInt();
                int xMonth = _DateTime.Substring(5, 2).ToInt();
                int xDay = _DateTime.Substring(8, 2).ToInt();

                int xHour = 0;
                int xMinute = 0;
                int xSecond = 0;
                if (_DateTime.Split(' ').Length > 1)
                {
                    var xTime = _DateTime.Split(' ')[1].Split(':');
                    xHour = xTime[0].ToInt();
                    xMinute = xTime[1].ToInt();
                    xSecond = xTime[2].ToInt();
                }

                DateTime _GregorianDate = _PersianCalendar.ToDateTime(
                    xYear,
                    xMonth,
                    xDay,
                    xHour,
                    xMinute,
                    xSecond,
                    0
                    );
                return _GregorianDate;
            }
            catch (Exception ex)
            {
                return DateTime.MinValue;
            }
        }
        public static DateTime ConvertToGregorian(object _DateTime)
        {
            try
            {
                return ConvertToGregorian(DateTime.Parse(_DateTime.ToString()));
            }
            catch (Exception ex)
            {
                return DateTime.MinValue;
            }
        }
        public static string ConvertToArabic(DateTime _DateTime, string _Format = "{Year}/{Month}/{Day}")
        {
            try
            {
                HijriCalendar _HijriCalendar = new HijriCalendar();
                string _strHijriDate = null;
                _strHijriDate = _Format.ToLower().Replace("{Year}".ToLower(), _HijriCalendar.GetYear(_DateTime).ToString("0000"))
                                .Replace("{Month}".ToLower(), _HijriCalendar.GetMonth(_DateTime).ToString("00"))
                                .Replace("{Day}".ToLower(), _HijriCalendar.GetDayOfMonth(_DateTime).ToString("00"));
                return _strHijriDate;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string ConvertToPersianString(object _DateTime, string _Format = "{DayOfWeek} {Day} {DayName} {Month} {MonthName} {Year}")
        {
            try
            {
                if (_DateTime == null || _DateTime.ToString() == "")
                {
                    return "";
                }

                DateTime _DateTimeGregorian = Convert.ToDateTime(_DateTime);
                PersianCalendar _PersianCalendar = new PersianCalendar();
                string _DayOfWeek = "";
                switch (_DateTimeGregorian.DayOfWeek)
                {
                    case DayOfWeek.Friday:
                        _DayOfWeek = "جمعه";
                        break;
                    case DayOfWeek.Monday:
                        _DayOfWeek = "دوشنبه";
                        break;
                    case DayOfWeek.Saturday:
                        _DayOfWeek = "شنبه";
                        break;
                    case DayOfWeek.Sunday:
                        _DayOfWeek = "یکشنبه";
                        break;
                    case DayOfWeek.Thursday:
                        _DayOfWeek = "پنجشنبه";
                        break;
                    case DayOfWeek.Tuesday:
                        _DayOfWeek = "سه شنبه";
                        break;
                    case DayOfWeek.Wednesday:
                        _DayOfWeek = "چهارشنبه";
                        break;
                }

                string _MonthName = "";
                switch (_PersianCalendar.GetMonth(_DateTimeGregorian))
                {
                    case 01:
                        _MonthName = "فروردین";
                        break;
                    case 02:
                        _MonthName = "اردیبهشت";
                        break;
                    case 03:
                        _MonthName = "خرداد";
                        break;
                    case 04:
                        _MonthName = "تیر";
                        break;
                    case 05:
                        _MonthName = "مرداد";
                        break;
                    case 06:
                        _MonthName = "شهریور";
                        break;
                    case 07:
                        _MonthName = "مهر";
                        break;
                    case 08:
                        _MonthName = "آبان";
                        break;
                    case 09:
                        _MonthName = "آذر";
                        break;
                    case 10:
                        _MonthName = "دی";
                        break;
                    case 11:
                        _MonthName = "بهمن";
                        break;
                    case 12:
                        _MonthName = "اسفند";
                        break;
                }

                string _DayName = "";
                switch (_PersianCalendar.GetDayOfMonth(_DateTimeGregorian))
                {
                    case 01:
                        _DayName = "یکم";
                        break;
                    case 02:
                        _DayName = "دوم";
                        break;
                    case 03:
                        _DayName = "سوم";
                        break;
                    case 04:
                        _DayName = "چهارم";
                        break;
                    case 05:
                        _DayName = "پنجم";
                        break;
                    case 06:
                        _DayName = "ششم";
                        break;
                    case 07:
                        _DayName = "هفتم";
                        break;
                    case 08:
                        _DayName = "هشتم";
                        break;
                    case 09:
                        _DayName = "نهم";
                        break;
                    case 10:
                        _DayName = "دهم";
                        break;
                    case 11:
                        _DayName = "یازدهم";
                        break;
                    case 12:
                        _DayName = "دوازدهم";
                        break;
                    case 13:
                        _DayName = "سیزدهم";
                        break;
                    case 14:
                        _DayName = "چهاردهم";
                        break;
                    case 15:
                        _DayName = "پانزدهم";
                        break;
                    case 16:
                        _DayName = "شانزدهم";
                        break;
                    case 17:
                        _DayName = "هفدهم";
                        break;
                    case 18:
                        _DayName = "هجدهم";
                        break;
                    case 19:
                        _DayName = "نوزدهم";
                        break;
                    case 20:
                        _DayName = "بیستم";
                        break;
                    case 21:
                        _DayName = "بیست و یکم";
                        break;
                    case 22:
                        _DayName = "بیست و دوم";
                        break;
                    case 23:
                        _DayName = "بیست و سوم";
                        break;
                    case 24:
                        _DayName = "بیست و چهارم";
                        break;
                    case 25:
                        _DayName = "بیست و پنجم";
                        break;
                    case 26:
                        _DayName = "بیست و ششم";
                        break;
                    case 27:
                        _DayName = "بیست و هفتم";
                        break;
                    case 28:
                        _DayName = "بیست و هشتم";
                        break;
                    case 29:
                        _DayName = "بیست و نهم";
                        break;
                    case 30:
                        _DayName = "سیم";
                        break;
                    case 31:
                        _DayName = "سی و یکم";
                        break;
                }

                string _PersianDate = null;
                _PersianDate = _Format.ToLower().Replace("{Year}".ToLower(), _PersianCalendar.GetYear(_DateTimeGregorian).ToString("0000"))
                                .Replace("{Month}".ToLower(), _PersianCalendar.GetMonth(_DateTimeGregorian).ToString("00"))
                                .Replace("{Day}".ToLower(), _PersianCalendar.GetDayOfMonth(_DateTimeGregorian).ToString("00"))
                                .Replace("{DayOfWeek}".ToLower(), _DayOfWeek)
                                .Replace("{MonthName}".ToLower(), _MonthName)
                                .Replace("{DayName}".ToLower(), _DayName);
                return _PersianDate;
            }
            catch (Exception ex)
            {
                //Logging.LogError(ex);
                return "";
            }
        }
        public static int ConvertToInt(this DayOfWeek xDayOfWeek)
        {
            switch (xDayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return 1;
                case DayOfWeek.Sunday:
                    return 2;
                case DayOfWeek.Monday:
                    return 3;
                case DayOfWeek.Tuesday:
                    return 4;
                case DayOfWeek.Wednesday:
                    return 5;
                case DayOfWeek.Thursday:
                    return 6;
                default:
                    return 7;
            }
        }

        public static byte ConvertTobyte(this DayOfWeek xDayOfWeek)
        {
            switch (xDayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return 1;
                case DayOfWeek.Sunday:
                    return 2;
                case DayOfWeek.Monday:
                    return 3;
                case DayOfWeek.Tuesday:
                    return 4;
                case DayOfWeek.Wednesday:
                    return 5;
                case DayOfWeek.Thursday:
                    return 6;
                default:
                    return 7;
            }
        }
    }
}
