using System;
using System.Collections.Generic;
using Xamarin.Forms;
using UXDivers.Grial;
using Ubi.Controls.Voip;

namespace Ubi
{
    public partial class ChatTimelineOutgoingItemTemplate : ContentView
    {
        public ChatTimelineOutgoingItemTemplate()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await VoipControler.getInstance(null).getHomePage().DisplayAlert("Mais detalhes", ((ChatMessageData)BindingContext).ExtraInfo, "Entendido");
        }
    }
}
