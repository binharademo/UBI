using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubi.ViewModels.Navigation.Dashboards;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ubi.Views.Navigation.Dashboards
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();

            BindingContext = new DashboardViewModel();
        }
    }
}