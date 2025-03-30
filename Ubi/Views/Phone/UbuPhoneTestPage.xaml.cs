using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using System.Linq;
using Linphone;

using UbiModelShared.Mock;

namespace Ubi.Views.Phone
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UbuPhoneTestPage : ContentPage
	{

	    private Core LinphoneCore
	    {
	        get
	        {
	            return ((App) App.Current).LinphoneCore;
	        }
	    }
	    private CoreListener Listener;

        public UbuPhoneTestPage ()
		{
			InitializeComponent ();

		    Listener = Factory.Instance.CreateCoreListener();
		    Listener.OnRegistrationStateChanged = OnRegistration;
		    Listener.OnCallStateChanged = OnCall;
		    Listener.OnCallStatsUpdated = OnStats;
		    LinphoneCore.AddListener(Listener);

            //TODO: Esses mock tem que ser trocados pelo ingetor de dependencias 
		    //username.Text = MockUserVoip.Lista().ElementAt(1).User;
		    password.Text = MockUserVoip.Lista().ElementAt(1).PassWord;
            domain.Text = MockUserVoip.Lista().ElementAt(1).ServerVoip;

		    address.Text = "0048991047346";
            redirect.Text = "2002";

        }

	    private void OnRegistration(Core lc, ProxyConfig config, RegistrationState state, string message)
	    {   
	        registration_status.Text = "Mudança no estado de registro: " + state;

	        if (state == RegistrationState.Ok)
	        {
	            register.IsEnabled = false;
	            this.FindByName<StackLayout>("stack_registrar").IsVisible = false;
	        }
	    }

        Call n1;
        Call n2;

        private void OnCall(Core lc, Call lcall, CallState state, string message)
        {
            
            call_status.Text = "Status da chamada alterada: " + state;

            if (lc.CallsNb > 0)
            {
                if (lc.CallsNb > 1) {
                    if (state == CallState.Connected) {
                        n2 = lcall;
                        LinphoneCore.TransferCallToAnother(n2, n1);
                    }
                } else {
                    if (state == CallState.Connected) { 
                        n1 = lcall;
                    }
                }
                if (state == CallState.IncomingReceived)
                {
                    call.Text = "Atendimento de Chamada (" + lcall.RemoteAddressAsString + ")";
                }
                else
                {
                    call.Text = "Terminar chamada";
                }
                if (lcall.CurrentParams.VideoEnabled)
                {
                    video.Text = "Parar o Video";
                }
                else
                {
                    video.Text = "Start Video";
                }
            }
            else
            {
                call.Text = "Iniciar Chamada";
                call_stats.Text = "";
            }
        }

        private void OnStats(Core lc, Call call, CallStats stats)
        {
            
            call_stats.Text = "Status da Chamada: " + stats.DownloadBandwidth + " kbits/s / " + stats.UploadBandwidth + " kbits/s";
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            var authInfo = Factory.Instance.CreateAuthInfo(username.Text, null, password.Text, null, null, domain.Text);
            LinphoneCore.AddAuthInfo(authInfo);

            var proxyConfig = LinphoneCore.CreateProxyConfig();
            var identity = Factory.Instance.CreateAddress("sip:sample@domain.tld");
            identity.Username = username.Text;
            identity.Domain = domain.Text;
            proxyConfig.Edit();
            proxyConfig.IdentityAddress = identity;
            proxyConfig.ServerAddr = domain.Text;
            proxyConfig.Route = domain.Text;
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
                
                var addr = LinphoneCore.InterpretUrl(address.Text);
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

        private void Callredirect_Clicked(object sender, EventArgs e) {
            //"2002"

            var addr = LinphoneCore.InterpretUrl(redirect.Text);
            LinphoneCore.InviteAddress(addr);

        }
    }
}