using FinStat.Domain.Enums;

namespace FinStat.Mobile.Utils
{
    public static class Navigator
    {
        public static RootPage CurrentPage { get; set; } = RootPage.Main;

        public static void ResetNavigation()
        {
            CurrentPage = RootPage.Main;
        }
    }
}
