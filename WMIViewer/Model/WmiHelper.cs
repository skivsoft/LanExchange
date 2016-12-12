using System;
using System.ComponentModel;
using System.Globalization;
using System.Management;

namespace WMIViewer.Model
{
    [Localizable(false)]
    internal static class WmiHelper
    {
        internal static DateTime ToDateTime(string wmiDate)
        {
            var initializer = DateTime.MinValue;
            int year = initializer.Year;
            int month = initializer.Month;
            int day = initializer.Day;
            int hour = initializer.Hour;
            int minute = initializer.Minute;
            int second = initializer.Second;
            long ticks = 0;
            var dmtf = wmiDate;
            string tempString;
            if (dmtf == null)
            {
                throw new ArgumentOutOfRangeException(nameof(wmiDate));
            }
            if (dmtf.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(wmiDate));
            }
            if (dmtf.Length != 25)
            {
                throw new ArgumentOutOfRangeException(nameof(wmiDate));
            }
            try
            {
                tempString = dmtf.Substring(0, 4);
                if (string.CompareOrdinal("****", tempString) != 0)
                {
                    year = int.Parse(tempString, CultureInfo.InvariantCulture);
                }
                tempString = dmtf.Substring(4, 2);
                if (string.CompareOrdinal("**", tempString) != 0)
                {
                    month = int.Parse(tempString, CultureInfo.InvariantCulture);
                }
                tempString = dmtf.Substring(6, 2);
                if (string.CompareOrdinal("**", tempString) != 0)
                {
                    day = int.Parse(tempString, CultureInfo.InvariantCulture);
                }
                tempString = dmtf.Substring(8, 2);
                if (string.CompareOrdinal("**", tempString) != 0)
                {
                    hour = int.Parse(tempString, CultureInfo.InvariantCulture);
                }
                tempString = dmtf.Substring(10, 2);
                if (string.CompareOrdinal("**", tempString) != 0)
                {
                    minute = int.Parse(tempString, CultureInfo.InvariantCulture);
                }
                tempString = dmtf.Substring(12, 2);
                if (string.CompareOrdinal("**", tempString) != 0)
                {
                    second = int.Parse(tempString, CultureInfo.InvariantCulture);
                }
                tempString = dmtf.Substring(15, 6);
                if (string.CompareOrdinal("******", tempString) != 0)
                {
                    ticks = long.Parse(tempString, CultureInfo.InvariantCulture) * (TimeSpan.TicksPerMillisecond / 1000);
                }
                if ((((((((year < 0)
                            || (month < 0))
                            || (day < 0))
                            || (hour < 0))
                            || (minute < 0))
                            || (minute < 0))
                            || (second < 0))
                            || (ticks < 0))
                {
                    throw new ArgumentOutOfRangeException(nameof(wmiDate));
                }
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            var datetime = new DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            var tickOffset = TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            int utcOffset;
            long offsetMins = tickOffset.Ticks / TimeSpan.TicksPerMinute;
            tempString = dmtf.Substring(22, 3);
            if (string.CompareOrdinal(tempString, "******") == 0)
                return datetime;

            tempString = dmtf.Substring(21, 4);
            try
            {
                utcOffset = int.Parse(tempString, CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                throw new ArgumentOutOfRangeException(null, e.Message);
            }
            var offsetToBeAdjusted = (int)(offsetMins - utcOffset);
            datetime = datetime.AddMinutes(offsetToBeAdjusted);
            return datetime;
        }

        internal static bool HasImplementedMethod(ManagementClass wmiClass)
        {
            foreach (var md in wmiClass.Methods)
                foreach (var qd in md.Qualifiers)
                    if (qd.Name.Equals("Implemented"))
                        return true;
            return false;
        }
    }
}
