﻿using System;
using System.Globalization;

namespace FinStat.Resources.Localization
{
    public static class Loc
    {
        public static string Text(Enum enumValue)
        {
            return Text($"{enumValue.GetType().Name}_{enumValue}");
        }

        public static string Text(string resourceKey, params object[] resourceParams)
        {
            if (resourceKey == null)
                throw new ArgumentNullException(nameof(resourceKey));

            var cultureInfo = CultureInfo.DefaultThreadCurrentUICulture;
            var resourceManager = Presentation.ResourceManager;
            var translation = resourceManager.GetString(resourceKey, cultureInfo);
            if (translation == null)
                return $"!{resourceKey}";

            return string.Format(cultureInfo, translation, resourceParams);
        }
    }
}
