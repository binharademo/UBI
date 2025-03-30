using Xamarin.Forms;
using UXDivers.Grial;
using System;

namespace Ubi
{
    public partial class DashboardAppNotificationItemTemplate : DashboardItemTemplateBase
    {
        public DashboardAppNotificationItemTemplate()
        {
            InitializeComponent();
        }

        protected override void OnTapped(object sender, EventArgs e)
        {
            Application.Current.MainPage.DisplayAlert("Tile Tapped! binhara2", "You have tapped a DashboardAppNotificationItemTemplate", "OK");
        }
    }
}