﻿using FinStat.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FinStat.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}