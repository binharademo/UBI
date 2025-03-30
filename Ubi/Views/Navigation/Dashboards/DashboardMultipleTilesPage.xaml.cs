using Xamarin.Forms;
using UXDivers.Grial;

namespace Ubi
{
    public partial class DashboardMultipleTilesPage : ContentPage
    {
        public DashboardMultipleTilesPage()
        {
            InitializeComponent();
            MyProgressBar.ProgressTo(0.1, 500, Easing.Linear);

            BindingContext = new DashboardMultipleTilesViewModel();
        }
    }
}