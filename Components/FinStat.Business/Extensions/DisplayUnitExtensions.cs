using System;
using FinStat.Domain.Enums;

namespace FinStat.Business.Extensions
{
    public static class DisplayUnitExtensions
    {
        public static double ToUnit(this DisplayUnit displayUnit)
        {
            switch (displayUnit)
            {
                case DisplayUnit.Default:
                    return 1.0;
                case DisplayUnit.Thousand:
                    return Math.Pow(10, 3);
                case DisplayUnit.Million:
                    return Math.Pow(10, 6);
                case DisplayUnit.Billion:
                    return Math.Pow(10, 9);
                default:
                    throw new ArgumentOutOfRangeException(nameof(displayUnit));
            }
        }
    }
}
