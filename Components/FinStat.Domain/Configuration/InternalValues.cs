using FinStat.Domain.Enums;

namespace FinStat.Domain.Configuration
{
    public static class InternalValues
    {
        public static InternalValue<DisplayUnit> DefaultDisplayUnit { get; } = new InternalValue<DisplayUnit>("DefaultDisplayUnit", DisplayUnit.Million);
    }
}
