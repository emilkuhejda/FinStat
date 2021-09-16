using System;
using FinStat.Domain.Enums;
using FinStat.Domain.Models;

namespace FinStat.Mobile.ViewModels.DataGrid
{
    public class ParameterDefinition
    {
        public ParameterDefinition(string title, Func<IncomeStatement, DisplayUnit, string> value)
        {
            Title = title;
            Value = value;
        }

        public string Title { get; }

        public Func<IncomeStatement, DisplayUnit, string> Value { get; }
    }
}
