using System;

namespace DK.Utility
{
    public static class Sys
    {
        public static int ToInt(this object _str)
        {
            if (_str == null)
                _str = string.Empty;

            if (_str.ToString().Contains('.'))
                _str = _str.ToString().Split('.')[0];

            if (_str.ToString().Contains(','))
                _str = _str.ToString().Replace(",", "");

            Int32 _Result = 0;
            Int32.TryParse(_str.ToString(), out _Result);
            return _Result;
        }
        public static decimal ToDecimal(this object _str)
        {
            if (_str == null)
                _str = string.Empty;
            Decimal _Result = 0;
            Decimal.TryParse(_str.ToString(), out _Result);
            return _Result;
        }

        public static decimal ToCeiling(this decimal _str)
        {
            if (_str == 0)
                return 0;
            return decimal.Ceiling(_str);
        }

        public static long ToLong(this object _str)
        {
            if (_str == null)
                _str = string.Empty;
            Int64 _Result = 0;
            Int64.TryParse(_str.ToString(), out _Result);
            return _Result;
        }
        public static bool ToBoolean(this object _str)
        {
            if (_str == null)
                _str = false;

            if (_str.ToString().Trim().ToLower() == "1")
                return true;
            if (_str.ToString().Trim().ToLower() == "0")
                return false;

            if (_str.ToString().Trim().ToLower() == "true")
                return true;
            if (_str.ToString().Trim().ToLower() == "false")
                return false;

            bool _Result = false;
            Boolean.TryParse(_str.ToString(), out _Result);
            return _Result;
        }
        public static bool IsNumeric(this string _str)
        {
            if (_str == null)
                _str = string.Empty;
            Decimal output = 0;
            return Decimal.TryParse(_str, out output);
        }
        public static string NumberToString(this object _str)
        {
            if (_str == null)
                _str = "";
            return new PNumberTString().num2str(_str.ToDecimal().ToString("0.#"));
        }

        public static byte ToByte(this object _str)
        {
            if (_str == null)
                _str = string.Empty;

            byte _Result = 0;
            byte.TryParse(_str.ToString(), out _Result);
            return _Result;
        }
        public static bool IsReady(this string xText)
        {
            if (string.IsNullOrWhiteSpace(xText))
                return false;

            return true;
        }

        public static bool IsReady(this int? xNumber)
        {
            if (xNumber == null)
                return false;

            return true;
        }

        public static bool IsReady(this DateTime? xDate)
        {
            if (xDate == null)
                return false;

            return true;
        }

        public static bool IsReady(this long? xNumber)
        {
            if (xNumber == null)
                return false;

            return true;
        }
    }

    class PNumberTString
    {
        private static string[] yakan = new string[10] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static string[] dahgan = new string[10] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static string[] dahyek = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        private static string[] sadgan = new string[10] { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static string[] basex = new string[5] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };
        private static string getnum3(int num3)
        {
            string s = "";
            int d3, d12;
            d12 = num3 % 100;
            d3 = num3 / 100;
            if (d3 != 0)
                s = sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + dahyek[d12 - 10];
            }
            else
            {
                int d2 = d12 / 10;
                if (d2 != 0)
                    s = s + dahgan[d2] + " و ";
                int d1 = d12 % 10;
                if (d1 != 0)
                    s = s + yakan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            };
            return s;
        }
        public string num2str(string snum)
        {
            string stotal = "";
            if (snum == "") return "صفر";
            if (snum == "0")
            {
                return yakan[0];
            }
            else
            {
                snum = snum.PadLeft(((snum.Length - 1) / 3 + 1) * 3, '0');
                int L = snum.Length / 3 - 1;
                for (int i = 0; i <= L; i++)
                {
                    int b = snum.Substring(i * 3, 3).ToInt();
                    if (b != 0)
                        stotal = stotal + getnum3(b) + " " + basex[L - i] + " و ";
                }
                stotal = stotal.Substring(0, stotal.Length - 3);
            }
            return stotal;
        }
    }
}