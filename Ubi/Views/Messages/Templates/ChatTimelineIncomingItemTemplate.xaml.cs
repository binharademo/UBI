using System;
using System.Collections.Generic;
using Xamarin.Forms;
using UXDivers.Grial;
using Ubi.Controls.Voip;
using UbiModelShared.Poco.Chat;

namespace Ubi
{
    public partial class ChatTimelineIncomingItemTemplate : ContentView
    {

        public ChatTimelineIncomingItemTemplate()
        {
            InitializeComponent();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try {
                await ChatTimelinePage.getInstance().DisplayAlert("Mais detalhes", ((ChatMessageData)BindingContext).ExtraInfo, "Entendido");
            }catch(Exception ee){
                await ChatTimelinePage.getInstance().DisplayAlert("Erro", ee.Message, "ok");
            }
        }


        private void OpenCurrentUserInformationPopUp (object sender, EventArgs e)
        {

            ChatTimelinePage.getInstance().OpenCurrentModal(((ChatMessageData)BindingContext).Step);

        }
    }
}
