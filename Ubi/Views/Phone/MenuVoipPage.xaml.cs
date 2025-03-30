using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ubi.Views.Phone
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuVoipPage : ContentPage
    {
        public MenuVoipPage()
        {
            InitializeComponent();
        }

        private void BtnVoipTeste_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UbuPhoneTestPage());
        }

        private void BtnReceber_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CallVoipPage());
        }

        private void BtnVoipTesteDual_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UbiPhoneTesteDualPage());
        }
    }
}