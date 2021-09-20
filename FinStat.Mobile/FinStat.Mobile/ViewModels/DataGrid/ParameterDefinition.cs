using System;
using FinStat.Domain.Enums;

namespace FinStat.Mobile.ViewModels.DataGrid
{
    public class ParameterDefinition<T>
    {
        public ParameterDefinition(string title, Func<T, DisplayUnit, string> value)
        {
            Title = title;
            Value = value;
        }

        public string Title { get; }

        public Func<T, DisplayUnit, string> Value { get; }
    }
}
