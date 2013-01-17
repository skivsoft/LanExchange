using System;
using System.Globalization;

namespace LanExchange.WMI
{
    public static class WMIUtils
    {
        public static DateTime ToDateTime(string dmtfDate)
        {
            var initializer = DateTime.MinValue;
            int year = initializer.Year;
            int month = initializer.Month;
            int day = initializer.Day;
            int hour = initializer.Hour;
            int minute = initializer.Minute;
            int second = initializer.Second;
            long ticks = 0;
            var dmtf = dmtfDate;
            string tempString;
            if (dmtf == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (dmtf.Length == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (dmtf.Length != 25)
            {
                throw new ArgumentOutOfRangeException();
            }
            try
            {
                tempString = dmtf.Substring(0, 4);
                if ((String.CompareOrdinal("****", tempString) != 0))
                {
                    year = int.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if ((String.CompareOrdinal("**", tempString) != 0))
                {
                    month = int.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if ((String.CompareOrdinal("**", tempString) != 0))
                {
                    day = int.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if ((String.CompareOrdinal("**", tempString) != 0))
                {
                    hour = int.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if ((String.CompareOrdinal("**", tempString) != 0))
                {
                    minute = int.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if ((String.CompareOrdinal("**", tempString) != 0))
                {
                    second = int.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if ((String.CompareOrdinal("******", tempString) != 0))
                {
                    ticks = (long.Parse(tempString) * (TimeSpan.TicksPerMillisecond / 1000));
                }
                if (((((((((year < 0)
                            || (month < 0))
                            || (day < 0))
                            || (hour < 0))
                            || (minute < 0))
                            || (minute < 0))
                            || (second < 0))
                            || (ticks < 0)))
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            DateTime datetime = new DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            var tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            int utcOffset;
            long OffsetMins = tickOffset.Ticks / TimeSpan.TicksPerMinute;
            tempString = dmtf.Substring(22, 3);
            if (String.CompareOrdinal(tempString, "******") == 0)
                return datetime;

            tempString = dmtf.Substring(21, 4);
            try
            {
                utcOffset = int.Parse(tempString);
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            int OffsetToBeAdjusted = ((int)((OffsetMins - utcOffset)));
            datetime = datetime.AddMinutes(OffsetToBeAdjusted);
            return datetime;
        }

        public static string ToDmtfDateTime(DateTime date)
        {
            string utcString;
            var tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = tickOffset.Ticks / TimeSpan.TicksPerMinute;
            if ((Math.Abs(OffsetMins) > 999))
            {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else
            {
                if ((tickOffset.Ticks >= 0))
                {
                    utcString = string.Concat("+", (tickOffset.Ticks / TimeSpan.TicksPerMinute).ToString(CultureInfo.InvariantCulture).PadLeft(3, '0'));
                }
                else
                {
                    string strTemp = OffsetMins.ToString(CultureInfo.InvariantCulture);
                    utcString = string.Concat("-", strTemp.Substring(1, (strTemp.Length - 1)).PadLeft(3, '0'));
                }
            }
            string dmtfDateTime = date.Year.ToString(CultureInfo.InvariantCulture).PadLeft(4, '0');
            dmtfDateTime = string.Concat(dmtfDateTime, date.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Day.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Hour.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Minute.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, date.Second.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ".");
            var dtTemp = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            long microsec = ((date.Ticks - dtTemp.Ticks)
                             * 1000)
                            / TimeSpan.TicksPerMillisecond;
            string strMicrosec = microsec.ToString(CultureInfo.InvariantCulture);
            if ((strMicrosec.Length > 6))
            {
                strMicrosec = strMicrosec.Substring(0, 6);
            }
            dmtfDateTime = string.Concat(dmtfDateTime, strMicrosec.PadLeft(6, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, utcString);
            return dmtfDateTime;
        }
    }
}
