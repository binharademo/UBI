using System;
using Xamarin.Forms;
using UXDivers.Grial;
using Ubi.Controls.Voip;

namespace Ubi
{
    public partial class MainMenuPage : ContentPage
    {
        public MainMenuPage(Action<Page> openPageAsRoot)
        {
            InitializeComponent();
            tfUserName.Text = "" + VoipControler.getInstance(null).GetUserVoip().FullName;
            BindingContext = new MainMenuViewModel(Navigation, openPageAsRoot);
        }
    }
}