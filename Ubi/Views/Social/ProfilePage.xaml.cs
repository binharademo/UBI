using Xamarin.Forms;
using UXDivers.Grial;

namespace Ubi
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();

            BindingContext = new ProfileViewModel();
        }
    }
}