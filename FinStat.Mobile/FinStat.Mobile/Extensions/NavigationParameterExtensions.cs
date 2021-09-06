using Prism.Navigation;

namespace FinStat.Mobile.Extensions
{
    internal static class NavigationParameterExtensions
    {
        public static void Add<T>(this INavigationParameters navigationParameters, object value)
        {
            navigationParameters.Add(typeof(T).Name, value);
        }

        public static T GetValue<T>(this INavigationParameters navigationParameters)
        {
            return navigationParameters.GetValue<T>(typeof(T).Name);
        }
    }
}
