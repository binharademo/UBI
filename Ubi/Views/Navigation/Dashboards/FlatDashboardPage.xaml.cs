using System;
using Xamarin.Forms;
using UXDivers.Grial;

namespace Ubi
{
    public partial class FlatDashboardPage : ContentPage
    {
        public FlatDashboardPage()
        {
            InitializeComponent();

            BindingContext = new NavigationViewModel(variantPageName: $"{this.GetType().Name}.xaml");
        }
    }
}