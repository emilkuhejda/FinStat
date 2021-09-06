using FinStat.Domain.Localization;

namespace FinStat.Resources.Localization
{
    public static class Languages
    {
        public static LanguageInfo English { get; } = new LanguageInfo(TranslationKeys.English, "en");

        public static LanguageInfo[] All => new[] { English };
    }
}
