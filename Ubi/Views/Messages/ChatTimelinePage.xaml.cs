using System;
using Xamarin.Forms;
using UbiModelShared.Validation;
using UbiModelShared.Poco.Chat;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using Ubi.Controls.Voip;
using Ubi.Views.DemoApp;
using Ubi.Views.Messages;
using UbiModelShared.Connection;
using UbiModelShared.Poco.Voip;
using UbiModelShared.Beans;

namespace Ubi
{
    public partial class ChatTimelinePage : ContentPage
    {
        //=-=-=-=-=-=- objetos das classes =-=-=-=-=-=-=-=-=-=-//
        private ChatBot _bot;
        private CallRealm call;
        private VoipControler vc;
        private ChatMessagesListViewModel _vm;
        private static ChatTimelinePage instance;

        //-=-=-=-= vari·veis dos usuarios do chat -=-=-=-=-=-=//
        public string userNameText = VoipControler.getInstance(null).GetUserVoip().Name; //"Pedro";
        private string userAvatarImageSource = "https://s3-us-west-2.amazonaws.com/grial-images/v3.0/friend_01.png";
        private const string ubiNameText = "Ubi";
        private const string ubiUserName = "Marcelo";
        private const string ubiAvatarImageSource = "app_icon.png";

        //=-=-==-=-=-= vari·veis do chat =-=-=-=-=-=-=-=-=-=-//

        public string mess = "";
        public List <string> userResponses;
        public string currentTime = DateTime.Now.ToString();
        public string UserBankName;

        //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=//

        public DateTimeOffset rescheduleDate;

        public ChatBot getChatBot(){
            return _bot;
        }

        //=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=//

        public ChatTimelinePage()
        {
            InitializeComponent();
            try {
                _bot = new ChatBot(userNameText, ubiUserName);
                instance = this;

                vc = VoipControler.getInstance(null);

                call = vc.GetCallRealm();

                _vm = new ChatMessagesListViewModel(variantPageName: $"{this.GetType().Name}.xaml");
                BindingContext = _vm;

                BotMessage bm = _bot.GetBotMessage();
                ChatMessageData cm = GetUbiMessage(bm.Question);
                cm.ExtraInfo = bm.AditionalInfo;
                cm.userName = _bot.username;
                cm.Step = _bot.Step;
                _vm.Messages.Add(cm);

                ((INotifyCollectionChanged)chatlist.ItemsSource).CollectionChanged += chatlist_CollectionChanged;

                if (rescheduleDate != null /*&& rescheduleDate != ""*/)
                {
                    buttonReschedule.IsEnabled = true;
                }
                else
                {
                    buttonReschedule.IsEnabled = false;
                }
            }catch(Exception e) {
                string error = e.StackTrace;
                DisplayAlert("Erro ao carregar", error, "OK");
            }
        }

        private void chatlist_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<ChatMessageData> someVar = ((IEnumerable<ChatMessageData>)this.chatlist.ItemsSource).ToList();
            chatlist.ScrollTo(someVar[someVar.Count - 1], ScrollToPosition.End, true);
        }

        public static ChatTimelinePage getInstance()
        {
            return instance;
        }

        private ChatMessageData GetUserMessage(string message)
        {
            ChatMessageData mess = new ChatMessageData();
            ChatUserData userdata = new ChatUserData();
            userdata.Name = userNameText;
            userdata.Avatar = userAvatarImageSource;
            mess.From = userdata;
            mess.Body = message;
            mess.userName = _bot.username;
            mess.Step = 0; // _bot.Step;
            mess.When = DateTime.Now.ToString();
            mess.IsRead = true;
            return mess;
        }

        private ChatMessageData GetUbiMessage(string message )
        {
            ChatMessageData mess = new ChatMessageData();
            ChatUserData userdata = new ChatUserData();
            userdata.Name = ubiNameText;
            userdata.Avatar = ubiAvatarImageSource;
            mess.From = userdata;
            mess.Body = message;
            mess.userName = _bot.username;
            mess.Step = _bot.Step;
            mess.When = DateTime.Now.ToString();
            mess.IsRead = true;
            if (call != null) { 
            Mensagem m = new Mensagem(ubiNameText, call.id, message, ubiAvatarImageSource, DateTimeOffset.Now);
            ConectionRealm.getInstance().AddMensagem(m);
            }
            return mess;
        }

        private ChatMessageData GetUbiMessageAdiciona(string message)
        {
            ChatMessageData mess = new ChatMessageData();
            ChatUserData userdata = new ChatUserData();
            userdata.Name = ubiNameText;
            userdata.Avatar = ubiAvatarImageSource;
            mess.From = userdata;
            mess.Body = message;
            mess.userName = _bot.username;
            mess.Step = _bot.Step;
            mess.When = DateTime.Now.ToString();
            mess.IsRead = true;
            Mensagem m = new Mensagem(ubiNameText, call.id, message, ubiAvatarImageSource, DateTimeOffset.Now);
            ConectionRealm.getInstance().AddMensagem(m);
            return mess;
        }

        // ========== M…TODOS DOS BOT’ES DA TELA DE CHAT ========= //

        public void BtSend_OnClicked(object sender, EventArgs e)
        {
            string texto = entryMessage.Text;
            //string messAdcional = _bot.GetAdicionaInformation();
            if (texto.ToUpper() == "SIM" || texto.ToUpper() == "S")
            {
                UserReplying("Sim");
            }
            else
            if (texto.ToUpper() == "NAO" || texto.ToUpper() == "N" || texto.ToUpper() == "N√O")
            {
                UserReplying("Nao");
            }
            else
            {
                _vm.Messages.Add(GetUserMessage(texto));
                _vm.Messages.Add(GetUbiMessage("Por favor, responda apenas com ''sim'' ou ''n„o'':\n" + mess));
            }
            CheckIfChatIsOver();
            //if(new IsNullOrEmptyRule<string>().Check( messAdcional))
            //_vm.Messages.Add(GetUbiMessage(messAdcional));
        }
        public void BtNo_OnClicked(object sender, EventArgs e)
        {
            UserReplying("Nao");
            CheckIfChatIsOver();
        }
        public void BtYes_OnClicked(object sender, EventArgs e)
        {
            UserReplying("Sim");
            CheckIfChatIsOver();
        }
        public async void BtCancel_OnClicked(object sender, EventArgs e)
        {
            bool confirmCancel = await DisplayAlert("Cancelar venda", "Tem certeza que deseja cancelar esta venda?", "Sim", "Nao");
            //bool confirmCancel = await DisplayAlert("Cancelar venda", "Tem certeza que deseja cancelar esta venda?", "Sim", "N„o");
            if (confirmCancel)
            {
                string confirmReason = await DisplayActionSheet("Qual o motivo do cancelamento?", "Voltar", null, "Cliente nao tem interesse", "Cliente nao atende o perfil", "Outro motivo");
                //string confirmReason = await DisplayActionSheet("Qual o motivo do cancelamento?", "Voltar", null, "Cliente n„o tem interesse", "Cliente n„o atende o perfil", "Outro motivo");
                if (confirmReason != null && confirmReason != "Voltar")
                {
                    FinishChat_Cancel(confirmReason);
                }
            }
        }
        public async void BtVender_OnClicked(object sender, EventArgs e) {
            bool confirmSell = await DisplayAlert("Confirmar venda", "Tem certeza que deseja confirmar esta venda?", "Sim", "Nao");
            //bool confirmSell = await DisplayAlert("Confirmar venda", "Tem certeza que deseja confirmar esta venda?", "Sim", "N„o");
            if (confirmSell){
                FinishChat_SuccessSell();
            }
        }

        public void Bt_OpenPopup_Reschedule(object sender, EventArgs e)
        {
            popupLayout.IsVisible = true;
            popupLayout.IsEnabled = true;
        }
        public void Bt_ClosePopup_Reschedule(object sender, EventArgs e)
        {
            popupLayout.IsVisible = false;
            popupLayout.IsEnabled = false;
        }
        public void SaveDate (object sender, DateChangedEventArgs e)
        {
           rescheduleDate = e.NewDate/*.ToString()*/;
        }
        public async void Reschedule(object sender, EventArgs e)
        {
            if (obsReschedule.Text == null || obsReschedule.Text == "")
            {
                bool confirmRescheduleWithoutObs = await DisplayAlert("Sem observacoes", "Reagendar venda para " + rescheduleDate + " sem observacoes?", "Sim", "Nao");
                //bool confirmRescheduleWithoutObs = await DisplayAlert("Sem observaÁıes", "Reagendar venda para " + rescheduleDate + " sem observaÁıes?", "Sim", "N„o");
                if (confirmRescheduleWithoutObs)
                {
                    FinishChat_Reschedule(rescheduleDate);
                }
            }
            else
            {
                bool confirmReschedule = await DisplayAlert("Reagendar", "Reagendamento para " + rescheduleDate + "?", "Sim", "Nao");
                //bool confirmReschedule = await DisplayAlert("Reagendar", "Reagendamento para " + rescheduleDate + "?", "Sim", "N„o");
                if (confirmReschedule)
                {
                    ConectionRealm.getInstance().ReScheduleCall(call, rescheduleDate, obsReschedule.Text);
                    FinishChat_Reschedule(rescheduleDate);
                }
            }
        }

        // ========== M…TODOS DOS BOT’ES DO POPUP DE PREENCHIMENTO DO USUARIO ========= //

        public void OkInfo_UserAdress(object sender, EventArgs e) {
            string userCEP = infoCEP.Text;
            string userHomeNumber = infoHomeNumber.Text;
            bool user_isComercialAdress = infoSameComercialAdress.IsChecked;
            popupUserAdress.IsVisible = false;
            transparentBackground.IsVisible = false;
            _vm.Messages.Add(GetUserMessage("Meu endereco:\n" + "CEP: " + userCEP + "\n"
                                            + "Numero: " + userHomeNumber));
            _vm.Messages.Add(GetUbiMessage(_bot.GetBotResponse(UserResponseEnum.Yes)));
        }
        public void CancelInfo_UserAdress(object sender, EventArgs e)
        {
            popupUserAdress.IsVisible = false;
            transparentBackground.IsVisible = false;
        }
        public void OkInfo_CantTalkToUser(object sender, EventArgs e)
        {
            bool userCanTalkLater = infoSameComercialAdress.IsChecked;
            popupCantTalkToUser.IsVisible = false;
            transparentBackground.IsVisible = false;
        }
        public void CancelInfo_CantTalkToUser(object sender, EventArgs e)
        {
            popupCantTalkToUser.IsVisible = false;
            transparentBackground.IsVisible = false;
        }
        public void SaveCantTalkToUserDate(object sender, DateChangedEventArgs e)
        {
            string newDateToTalkToUser = e.NewDate.ToString();
        }
        public void CheckBank(object sender, EventArgs e)
        {
            string bankName;
            switch (infoBank.SelectedIndex)
            {
                case 0:
                    anotherBankEntry.IsVisible = false;
                    bankName = "Banco do Brasil";
                    break;
                case 1:
                    anotherBankEntry.IsVisible = false;
                    bankName = "Bradesco";
                    break;
                case 2:
                    anotherBankEntry.IsVisible = false;
                    bankName = "Caixa";
                    break;
                case 3:
                    anotherBankEntry.IsVisible = false;
                    bankName = "Inter";
                    break;
                case 4:
                    anotherBankEntry.IsVisible = false;
                    bankName = "Ita˙";
                    break;
                case 5:
                    anotherBankEntry.IsVisible = false;
                    bankName = "Nubank";
                    break;
                case 6:
                    anotherBankEntry.IsVisible = false;
                    bankName = "Santander";
                    break;
                case 7:
                    anotherBankEntry.IsVisible = true;
                    bankName = anotherBankEntry.Text;
                    break;
                default:
                    anotherBankEntry.IsVisible = false;
                    bankName = "Nenhum banco selecionado";
                    break;
            }
            UserBankName = bankName;
        }
        public void OkInfo_Bank(object sender, EventArgs e)
        {
            //string userBank = infoBank.SelectedIndex.ToString; SALVAR STRING PICKER
            string userAgency = infoAgency.Text;
            string userAccountNumber = AccountNumber.Text;
            string userAccountDigit = AccountDigit.Text;
            popupBank.IsVisible = false;
            transparentBackground.IsVisible = false;
            _vm.Messages.Add(GetUserMessage("Meus dados banc·rios:\n" + "Banco: " + UserBankName + "\n"
                                           + "AgÍncia: " + userAgency + "\n"
                                           + "Conta: " + userAccountNumber + "-" + userAccountDigit));
            _vm.Messages.Add(GetUbiMessage(_bot.GetBotResponse(UserResponseEnum.Yes)));
        }
        public void CancelInfo_Bank(object sender, EventArgs e)
        {
            popupBank.IsVisible = false;
            transparentBackground.IsVisible = false;
        }
        public void OkInfo_Payment(object sender, EventArgs e)
        {
            string userMachine = infoMachine.Text;
            string userTaxes = infoTaxes.Text;
            popupPayment.IsVisible = false;
            transparentBackground.IsVisible = false;
            _vm.Messages.Add(GetUserMessage("Info. de pagamento:\n"
                                            + "Maquininha: " + userMachine + "\n"
                                            + "Taxas: " + userTaxes+"%"));
            _vm.Messages.Add(GetUbiMessage(_bot.GetBotResponse(UserResponseEnum.Yes)));
        }
        public void CancelInfo_Payment(object sender, EventArgs e)
        {
            popupPayment.IsVisible = false;
            transparentBackground.IsVisible = false;
        }       
        public void OkInfo_Comment(object sender, EventArgs e) {
            _vm.Messages.Add(GetUserMessage("Coment·rio:\n"+"''"+infoComment.Text +"''"));
            _vm.Messages.Add(GetUbiMessage(mess));
            transparentBackground.IsVisible = false;
            popupComment.IsVisible = false;
        }
        public void CancelInfo_Comment(object sender, EventArgs e) {
            transparentBackground.IsVisible = false;
            popupComment.IsVisible = false;
        }
        public void OpenCommentModal (object sender, EventArgs e )
        {
            transparentBackground.IsVisible = true;
            popupComment.IsVisible = true;
        }
       
        // =========================================================================== //
        public void OpenCurrentModal(int currentStep)
        {
            switch (currentStep)
            {
                case 4:
                    popupBank.IsVisible=true;
                    transparentBackground.IsVisible = true;

                    popupPayment.IsVisible = false;
                    popupUserAdress.IsVisible = false;
                    popupCantTalkToUser.IsVisible = false;
                    break;
                case 7:
                    popupPayment.IsVisible = true;
                    transparentBackground.IsVisible = true;

                    popupBank.IsVisible = false;
                    popupUserAdress.IsVisible = false;
                    popupCantTalkToUser.IsVisible = false;
                    break;
                case 10:
                    popupUserAdress.IsVisible = true;
                    transparentBackground.IsVisible = true;

                    popupBank.IsVisible = false;
                    popupPayment.IsVisible = false;
                    popupCantTalkToUser.IsVisible = false;
                    popupPayment.IsVisible = false;
                    break;
                case 21:
                    popupCantTalkToUser.IsVisible = true;
                    transparentBackground.IsVisible = true;

                    popupBank.IsVisible = false;
                    popupPayment.IsVisible = false;
                    popupUserAdress.IsVisible = false;
                    popupPayment.IsVisible = false;
                    break;
                case 22:
                    popupPayment.IsVisible = true;
                    transparentBackground.IsVisible = true;

                    popupBank.IsVisible = false;
                    popupUserAdress.IsVisible = false;
                    popupCantTalkToUser.IsVisible = false;
                    break;
                default:
                    transparentBackground.IsVisible = false;
                    popupBank.IsVisible = false;
                    popupPayment.IsVisible = false;
                    popupUserAdress.IsVisible = false;
                    popupCantTalkToUser.IsVisible = false;
                    popupPayment.IsVisible = false;
                    break;
            }
        }
        // =========================================================================== //

        public void UserReplying (string userReply)
        {
            UserMessagesResponse userMessage = new UserMessagesResponse();
            if (userReply.ToUpper() == "NAO")
            {
                mess = _bot.GetBotResponse(UserResponseEnum.No);
            }else
            if (userReply.ToUpper() == "SIM")
            {
                mess = _bot.GetBotResponse(UserResponseEnum.Yes);
            }
            _vm.Messages.Add(GetUserMessage(userReply));
            _vm.Messages.Add(GetUbiMessage(mess));
            userMessage.message = userReply;
            //call.UserResponses.Add(userMessage);
        }
        private async void FinishChat_SuccessSell()
        {
            ConectionRealm.getInstance().ConfirmCall(call);
            await DisplayAlert("Parabens", "Sua venda sera auditada em breve.", "Entendido");
            //await DisplayAlert("ParabÈns", "Sua venda serÅEauditada em breve.", "Entendido");
            vc.BackToHome();
        }
        private async void FinishChat_Cancel(string confirmation)
        {
            await DisplayAlert("Venda cancelada", "A UBI agradece seu feedback :)", "Concluir");
            ConectionRealm.getInstance().CancelCall(call, confirmation);
            vc.BackToHome();
        }
        private async void FinishChat_Reschedule(DateTimeOffset date)
        {
            ConectionRealm.getInstance().ReScheduleCall(call, rescheduleDate, "");
            call.reScheduledDate = date;
            call.state = 3; //ÅEesse call state?
            if (obsReschedule.Text != null && obsReschedule.Text != "")
            {
                call.reScheduledObservation = obsReschedule.Text;
            }
            await DisplayAlert("Sucesso!", "Sua venda foi reagendada", "Ok");
            popupLayout.IsEnabled = false;
            popupLayout.IsVisible = false;
            vc.BackToHome();
        }
   
        public async void CancelFinishChat()
        {
            bool confirmCancel = await DisplayAlert("Cancelar venda", "Tem certeza que deseja cancelar esta venda?", "Sim", "Nao");
            //bool confirmCancel = await DisplayAlert("Cancelar venda", "Tem certeza que deseja cancelar esta venda?", "Sim", "N„o");
            if (confirmCancel)
            {
                string confirmReason = await DisplayActionSheet("Qual o motivo do cancelamento?", "Voltar", null, "Cliente nao tem interesse", "Cliente nao atende o perfil", "Outro motivo");
                //string confirmReason = await DisplayActionSheet("Qual o motivo do cancelamento?", "Voltar", null, "Cliente n„o tem interesse", "Cliente n„o atende o perfil", "Outro motivo");
                if (confirmReason != null && confirmReason != "Voltar")
                {
                    FinishChat_Cancel(confirmReason);
                }
            }
        }     

        private async void CheckIfChatIsOver()
        {
            string popUpTitle = "A Ubi agradece!";
            switch (_bot.Step)
            {
                case 5:
                    FinishChat();
                    break;
                case 14:
                    await DisplayAlert(popUpTitle, "Assim que eu desligar essa chamada vocÍ vai receber um SMS com nossos contatos e, caso mude de ideia, pode nos chamar nesse numero. Obrigado e bom dia. [tarde] , [noite].", "Concluir");
                    //await DisplayAlert(popUpTitle, " Assim que eu desligar essa chamada vocÍ vai receber um SMS com nossos contatos e, caso mude de ideia, pode nos chamar nesse n˙mero. Obrigado e bom dia. [tarde] , [noite].", "Concluir");
                    //atualizar status realms
                    vc.BackToHome();
                    break;
                case 15:
                    await DisplayAlert(popUpTitle, "Porem, essa campanha e somente para conta PJ. Quando tivermos (futuramente) campanha para conta PF, entraremos em contato. Voce recebera um SMS com nossos telefones e caso venha a abrir uma conta PJ pode nos chamar.", "Concluir");
                    //await DisplayAlert(popUpTitle, "PorÈm, essa campanha È somente para conta PJ. Quando tivermos (futuramente) campanha para conta PF, entraremos em contato. VocÍ receber· um SMS com nossos telefones e caso venha a abrir uma conta PJ pode nos chamar.", "Concluir");
                    //atualizar status realms
                    vc.BackToHome();
                    break;
                case 16:
                    await DisplayAlert(popUpTitle, "Infelizmente, nao podemos vender para clientes que ja possuem maquina SIPAG mesmo que ele cancele a maquina, pois no Brasil existe um acordo entre essas empresas.", "Concluir");
                    //await DisplayAlert(popUpTitle, "Infelizmente, n„o podemos vender para clientes que j· possuem m·quina SIPAG mesmo que ele cancele a m·quina, pois no Brasil existe um acordo entre essas empresas.", "Concluir");
                    //atualizar status realms
                    vc.BackToHome();
                    break;
                case 17:
                    await DisplayAlert(popUpTitle, "Essa campanha e para clientes que faturem pelo menos 3 mil reais por mes na maquininha. Ao fim da ligacao voce recebera um SMS com nossos contatos e se voce conseguir subir o faturamento basta entrar em contato.", "Concluir");
                    //await DisplayAlert(popUpTitle, "Essa campanha È para clientes que faturem pelo menos 3 mil reais por mÍs na maquininha. Ao fim da ligaÁ„o vocÍ receber· um SMS com nossos contatos e se vocÍ conseguir subir o faturamento basta entrar em contato.", "Concluir");
                    //atualizar status realms
                    vc.BackToHome();
                    break;
                case 18:
                    await DisplayAlert(popUpTitle, "E possivel fazer entrega somente no endereco da empresa que consta no CNPJ. Caso contrario, infelizmente nao podemos continuar com a proposta. No final da chamada vocÍ recebera um SMS com nossos contatos e caso consiga fazer a alteraÁ„o, entre em contato imediatamente.", "Concluir");
                    //await DisplayAlert(popUpTitle, "… possÌvel fazer entrega somente no endereÁo da empresa que consta no CNPJ. Caso contr·rio, infelizmente n„o podemos continuar com a proposta. No final da chamada vocÍ receber· um SMS com nossos contatos e caso consiga fazer a alteraÁ„o, entre em contato imediatamente.", "Concluir");
                    //atualizar status realms
                    vc.BackToHome();
                    break;
                case 30:
                    FinishChat();
                    break;
                case 40:
                    //indefinido
                    break;
                case 41:
                    //input para dados de text
                    break;
            }
        }

        public async void FinishChat()
        {
            string confirmReason = await DisplayActionSheet("Chat finalizado", "Voltar", null, "Concluir venda", "Cancelar venda", "Reagenedar venda");
            if (confirmReason == "Concluir venda")
            {
                FinishChat_SuccessSell();
            }
            if (confirmReason == "Cancelar")
            {
                CancelFinishChat();
            }
            if (confirmReason == "Reagendar venda")
            {
                popupLayout.IsVisible = true;
                popupLayout.IsEnabled = true;
            }
        }

        
    }
}

