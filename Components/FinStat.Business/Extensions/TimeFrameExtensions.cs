using System;
using FinStat.Domain.Enums;

namespace FinStat.Business.Extensions
{
    public static class TimeFrameExtensions
    {
        public static string ToValue(this TimeFrame timeFrame)
        {
            switch (timeFrame)
            {
                case TimeFrame.Min1:
                    return "1min";
                case TimeFrame.Min5:
                    return "5min";
                case TimeFrame.Min15:
                    return "15min";
                case TimeFrame.Min30:
                    return "30min";
                case TimeFrame.Hour1:
                    return "1hour";
                case TimeFrame.Hour4:
                    return "4hour";
                default:
                    throw new ArgumentOutOfRangeException(nameof(timeFrame));
            }
        }
    }
}
