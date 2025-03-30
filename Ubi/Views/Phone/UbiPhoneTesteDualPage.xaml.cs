using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Linphone;

namespace Ubi.Views.Phone
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UbiPhoneTesteDualPage : ContentPage
    {
        private User User1;
        private User User2;
        private User UserSelect;
        private User userToCall;

        public UbiPhoneTesteDualPage()
        {
            InitializeComponent();

            Listener = Factory.Instance.CreateCoreListener();
            Listener.OnRegistrationStateChanged = OnRegistration;
            Listener.OnCallStateChanged = OnCall;
            Listener.OnCallStatsUpdated = OnStats;
            LinphoneCore.AddListener(Listener);

            User2 = new User();
            User2.Nome = "2003";
            User2.Senha = "1234";
            User2.ServerUrl = "170.245.200.107";

            User1 = new User();
            User1.Nome = "2002";
            User1.Senha = "1234";
            User1.ServerUrl = "170.245.200.107";

            OnRegisterClicked(null, null);
        }

        private Core LinphoneCore
        {
            get
            {
                return ((App)App.Current).LinphoneCore;
            }
        }
        private CoreListener Listener;

        private void OnRegistration(Core lc, ProxyConfig config, RegistrationState state, string message)
        {

            Console.WriteLine("Registration state changed: " + state);
            registration_status.Text = "Registration state changed: " + state;

            if (state == RegistrationState.Ok)
            {
                register.IsEnabled = false;
                //this.FindByName<StackLayout>("stack_registrar").IsVisible = false;
            }
        }

        private void OnCall(Core lc, Call lcall, CallState state, string message)
        {
            Console.WriteLine("Call state changed: " + state);
            call_status.Text = "Call state changed: " + state;

            if (lc.CallsNb > 0)
            {
                if (state == CallState.IncomingReceived)
                {
                    call.Text = "Answer Call (" + lcall.RemoteAddressAsString + ")";
                }
                else
                {
                    call.Text = "Terminate Call";
                }
                if (lcall.CurrentParams.VideoEnabled)
                {
                    //video.Text = "Stop Video";
                }
                else
                {
                    //video.Text = "Start Video";
                }
            }
            else
            {
                call.Text = "Start Call";
                call_stats.Text = "";
            }
        }

        private void OnStats(Core lc, Call call, CallStats stats)
        {

            Console.WriteLine("Call stats: " + stats.DownloadBandwidth + " kbits/s / " + stats.UploadBandwidth + " kbits/s");
            call_stats.Text = "Call stats: " + stats.DownloadBandwidth + " kbits/s / " + stats.UploadBandwidth + " kbits/s";
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            //var authInfo = Factory.Instance.CreateAuthInfo(username.Text, null, password.Text, null, null, domain.Text);
            var authInfo = Factory.Instance.CreateAuthInfo(UserSelect.Nome, null, UserSelect.Senha, null, null, UserSelect.ServerUrl);


            LinphoneCore.AddAuthInfo(authInfo);

            var proxyConfig = LinphoneCore.CreateProxyConfig();
            var identity = Factory.Instance.CreateAddress("sip:sample@domain.tld");
            //identity.Username = username.Text;
            identity.Username = UserSelect.Nome;
            //identity.Domain = domain.Text;
            identity.Domain = UserSelect.ServerUrl;
            proxyConfig.Edit();
            proxyConfig.IdentityAddress = identity;
            //proxyConfig.ServerAddr = domain.Text;
            proxyConfig.ServerAddr = UserSelect.ServerUrl;
            //proxyConfig.Route = domain.Text;
            proxyConfig.Route = UserSelect.ServerUrl;
            proxyConfig.RegisterEnabled = true;
            proxyConfig.Done();
            LinphoneCore.AddProxyConfig(proxyConfig);
            LinphoneCore.DefaultProxyConfig = proxyConfig;

            LinphoneCore.RefreshRegisters();
        }

        private void OnCallClicked(object sender, EventArgs e)
        {
            if (LinphoneCore.CallsNb == 0)
            {
                Address addr = new Address();
                if (address.Text != null)
                    addr = LinphoneCore.InterpretUrl(address.Text);
                else
                    addr = LinphoneCore.InterpretUrl(userToCall.Nome);

                LinphoneCore.InviteAddress(addr);
            }
            else
            {
                Call call = LinphoneCore.CurrentCall;
                if (call.State == CallState.IncomingReceived)
                {
                    LinphoneCore.AcceptCall(call);
                }
                else
                {
                    LinphoneCore.TerminateAllCalls();
                }
            }
        }

        private void OnVideoClicked(object sender, EventArgs e)
        {
            if (LinphoneCore.CallsNb > 0)
            {
                Call call = LinphoneCore.CurrentCall;
                if (call.State == CallState.StreamsRunning)
                {
                    LinphoneCore.VideoAdaptiveJittcompEnabled = true;
                    CallParams param = LinphoneCore.CreateCallParams(call);
                    param.VideoEnabled = !call.CurrentParams.VideoEnabled;
                    param.VideoDirection = MediaDirection.SendRecv;
                    LinphoneCore.UpdateCall(call, param);
                }
            }
        }

        private void BtnUser01_OnClicked(object sender, EventArgs e)
        {
            btnUser01.BackgroundColor = Color.FromHex("818181");
            btnUser01.TextColor = Color.White;
            btnUser02.BackgroundColor = Color.White;
            btnUser02.TextColor = Color.Black;
            call.Text = "Ligar para usuario2";
            UserSelect = User1;
            userToCall = User2;
            lbuserselect.Text = "Usuário: " + UserSelect.Nome;


        }

        private void BtnUser02_OnClicked(object sender, EventArgs e)
        {
            btnUser01.BackgroundColor = Color.White;
            btnUser01.TextColor = Color.Black;
            btnUser02.BackgroundColor = Color.FromHex("818181");
            btnUser02.TextColor = Color.White;
            call.Text = "Ligar para usuario1";
            UserSelect = User2;
            userToCall = User1;
            lbuserselect.Text = "Usuário: " + UserSelect.Nome;
        }

        private void Address_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            call.Text = address.Text;
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            address.IsVisible = true;
        }
    }

    public class User
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string ServerUrl { get; set; }

    }
}