using System;

namespace LanExchange.Utils
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
            string dmtf = dmtfDate;
            var datetime = DateTime.MinValue;
            string tempString = string.Empty;
            if ((dmtf == null))
            {
                throw new ArgumentOutOfRangeException();
            }
            if ((dmtf.Length == 0))
            {
                throw new ArgumentOutOfRangeException();
            }
            if ((dmtf.Length != 25))
            {
                throw new ArgumentOutOfRangeException();
            }
            try
            {
                tempString = dmtf.Substring(0, 4);
                if ((String.Compare("****", tempString, false) != 0))
                {
                    year = int.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if ((String.Compare("**", tempString, false) != 0))
                {
                    month = int.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if ((String.Compare("**", tempString, false) != 0))
                {
                    day = int.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if ((String.Compare("**", tempString, false) != 0))
                {
                    hour = int.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if ((String.Compare("**", tempString, false) != 0))
                {
                    minute = int.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if ((String.Compare("**", tempString, false) != 0))
                {
                    second = int.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if ((String.Compare("******", tempString, false) != 0))
                {
                    ticks = (long.Parse(tempString) * ((long)((TimeSpan.TicksPerMillisecond / 1000))));
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
            datetime = new DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            var tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            int UTCOffset = 0;
            int OffsetToBeAdjusted = 0;
            long OffsetMins = ((long)((tickOffset.Ticks / TimeSpan.TicksPerMinute)));
            tempString = dmtf.Substring(22, 3);
            if (String.Compare(tempString, "******", false) == 0)
                return datetime;

            tempString = dmtf.Substring(21, 4);
            try
            {
                UTCOffset = int.Parse(tempString);
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            OffsetToBeAdjusted = ((int)((OffsetMins - UTCOffset)));
            datetime = datetime.AddMinutes(((double)(OffsetToBeAdjusted)));
            return datetime;
        }

        public static string ToDmtfDateTime(DateTime date)
        {
            string utcString = string.Empty;
            var tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = ((long)((tickOffset.Ticks / TimeSpan.TicksPerMinute)));
            if ((Math.Abs(OffsetMins) > 999))
            {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else
            {
                if ((tickOffset.Ticks >= 0))
                {
                    utcString = string.Concat("+", ((Int64)((tickOffset.Ticks / TimeSpan.TicksPerMinute))).ToString().PadLeft(3, '0'));
                }
                else
                {
                    string strTemp = ((Int64)(OffsetMins)).ToString();
                    utcString = string.Concat("-", strTemp.Substring(1, (strTemp.Length - 1)).PadLeft(3, '0'));
                }
            }
            string dmtfDateTime = ((Int32)(date.Year)).ToString().PadLeft(4, '0');
            dmtfDateTime = string.Concat(dmtfDateTime, ((Int32)(date.Month)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((Int32)(date.Day)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((Int32)(date.Hour)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((Int32)(date.Minute)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ((Int32)(date.Second)).ToString().PadLeft(2, '0'));
            dmtfDateTime = string.Concat(dmtfDateTime, ".");
            var dtTemp = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            long microsec = ((long)((((date.Ticks - dtTemp.Ticks)
                        * 1000)
                        / TimeSpan.TicksPerMillisecond)));
            string strMicrosec = ((Int64)(microsec)).ToString();
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
