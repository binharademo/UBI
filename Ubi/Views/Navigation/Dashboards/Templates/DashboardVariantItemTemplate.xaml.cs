using Xamarin.Forms;
using UXDivers.Grial;
using System;

namespace Ubi
{
    public partial class DashboardVariantItemTemplate : DashboardItemTemplateBase
    {
        public DashboardVariantItemTemplate()
        {
            InitializeComponent();
        }

        protected override void OnTapped(object sender, EventArgs e)
        {
            Application.Current.MainPage.DisplayAlert("Tile Tapped!", "You have tapped a DashboardVariantItemTemplate", "OK");
        }
    }
}