using Linphone;
using System;
using System.Collections.Generic;
using System.Text;
using Ubi.Views.Messages;
using UbiModelShared.Beans;
using UbiModelShared.Connection;
using UbiModelShared.Poco.Voip;
using Xamarin.Forms;

namespace Ubi.Controls.Voip
{
    public class VoipControler {

        //private User UserSelect;

        private static VoipControler instance;
        private bool registered = false;
        public Label lb_status;

        public Page getHomePage()
        {
            return hp;
        }

        public bool receiving { get; set; }
        private CoreListener Listener;
        private CallRealm currentCallData;
        private UserVoip loggedUser;
        private string dialerpassword = "1964@jkf";
        private string dailerdomain = "170.245.200.107";



        private Core LinphoneCore {
            get {
                return ((App)App.Current).LinphoneCore;
            }
        }

        public static VoipControler getInstance(Page hp) {
            if (instance == null) {
                instance = new VoipControler(hp);
            }
            if (instance.hp == null)
            {
                instance.hp = hp;
            }
            return instance;
        }

        Page hp;

        private VoipControler(Page hp) {
            this.hp = hp;

            Listener = Factory.Instance.CreateCoreListener();
            Listener.OnRegistrationStateChanged = OnRegistration;
            Listener.OnCallStateChanged = OnCall;
            Listener.OnCallStatsUpdated = OnStats;
            LinphoneCore.AddListener(Listener);

            //Para questão de teste, usuario fixo
            //TODO carregar usuario relativo ao real usuario logado
            /*
            User User1 = new User();
            User1.Nome = "2002";
            User1.Senha = "1234";
            User1.ServerUrl = "170.245.200.107";

            UserSelect = User1;
            */

        }

        public void setLogin(UserVoip u)
        {
            loggedUser = u;
        }

        public UserVoip GetUserVoip()
        {
            
            return loggedUser;
        }

        ProxyConfig proxyConfig;
        public void Register() {
            if (!registered) {

                var authInfo = Factory.Instance.CreateAuthInfo(loggedUser.BranchLineNumber, null, dialerpassword, null, null, dailerdomain);

                LinphoneCore.AddAuthInfo(authInfo);

                proxyConfig = LinphoneCore.CreateProxyConfig();
                var identity = Factory.Instance.CreateAddress("sip:sample@domain.tld");
                identity.Username = loggedUser.BranchLineNumber;
                identity.Domain = dailerdomain;
                proxyConfig.Edit();
                proxyConfig.IdentityAddress = identity;
                proxyConfig.ServerAddr = dailerdomain;
                proxyConfig.Route = dailerdomain;
                proxyConfig.RegisterEnabled = true;
                proxyConfig.Done();
                LinphoneCore.AddProxyConfig(proxyConfig);
                LinphoneCore.DefaultProxyConfig = proxyConfig;

                LinphoneCore.RefreshRegisters();

                ConectionRealm.getInstance().setUserOnline(loggedUser, true);
            }
        }

        public void CancelRegister() {
            if (LinphoneCore.CallsNb != 0) {
                LinphoneCore.TerminateAllCalls();
            }
            if (proxyConfig != null) {
                LinphoneCore.RemoveProxyConfig(proxyConfig);
                registered = false;
            }
            ConectionRealm.getInstance().setUserOnline(loggedUser, false);
        }

        public CallRealm GetCallRealm()
        {
            
            if (currentCallData == null)
            {
                currentCallData = ConectionRealm.getInstance().getCallRealmFromUserVoip(loggedUser);
            }
            return currentCallData;
        }

        private void OnRegistration(Core lc, ProxyConfig config, RegistrationState state, string message) {

            Console.WriteLine("Registration state changed: " + state);

            if (state == RegistrationState.Ok) {
                registered = true;
                if (lb_status != null) {
                    lb_status.Text = "Atendimento Ativado";
                    lb_status.TextColor = Color.Green;
                    //fazendo a parte do ligador:
                    currentCallData = ConectionRealm.getInstance().getPendingCall(loggedUser);
                    if (currentCallData != null) {
                        Address addr = new Address();
                        addr = LinphoneCore.InterpretUrl(currentCallData.phone);
                        LinphoneCore.InviteAddress(addr);
                    } else {
                        lb_status.Text = "nenhuma call encontrada";
                        lb_status.TextColor = Color.Red;
                    }
                }
            } else {
                if (lb_status != null) {
                    lb_status.TextColor = Color.Red;
                }
            }
        }

        private void OnCall(Core lc, Linphone.Call lcall, CallState state, string message) {
            Console.WriteLine("Call state changed: " + state);
            lb_status.Text = "CallState: " + state;
            if (lc.CallsNb > 0) {
                if (state == CallState.IncomingReceived) {
                    //answerCall();
                    //                    call.Text = "Answer Call (" + lcall.RemoteAddressAsString + ")";
                }
                if(state == CallState.Connected) {
                    ConectionRealm.getInstance().setUserBusy(loggedUser, true);
                    hp.Navigation.PushAsync(new ChatTimelinePage());
                }
                if (state == CallState.End) {
                    ConectionRealm.getInstance().setUserBusy(loggedUser, false);
                }
//                 else {
//                    call.Text = "Terminate Call";
//                }
//                if (lcall.CurrentParams.VideoEnabled) {
//                    video.Text = "Stop Video";
//                } else {
//                    video.Text = "Start Video";
//                }
            }
        }

        private async void answerCall() {
            if (receiving) {
                string answer = await hp.DisplayActionSheet("Recebendo Chamada", "Recusar", "Atender");
                if (answer.Equals("Atender")) {
                    ReceiveCall();
                } else {
                    LinphoneCore.TerminateAllCalls();//caso recuse, terminate call
                }
            }
        }

        public void ReceiveCall() {
            if (LinphoneCore.CallsNb != 0) {
                Linphone.Call call = LinphoneCore.CurrentCall;
                if (call.State == CallState.IncomingReceived) {
                    LinphoneCore.AcceptCall(call);

                } else {
                    LinphoneCore.TerminateAllCalls();
                }
            }
        }

        public void BackToHome()
        {
            if (hp!=null)
            {
                hp.Navigation.PopAsync();
            }
        }

        private void OnStats(Core lc, Linphone.Call call, CallStats stats) {
            Console.WriteLine("Call stats: " + stats.DownloadBandwidth + " kbits/s / " + stats.UploadBandwidth + " kbits/s");
        }


        public class User {
            public string Nome { get; set; }
            public string Senha { get; set; }
            public string ServerUrl { get; set; }

        }
    }

}
