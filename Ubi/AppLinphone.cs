using System;
using System.Threading;
using Xamarin.Forms;
using Linphone;

namespace Ubi
{
    public partial class App : Application
    {
        public  Core LinphoneCore { get; set; }

        public App(string rc_path = null)
        {
            InitializeComponent();

            // Localization:
            //
            // Use "DefaultStringResources" key to define the default Resx type and get
            // the most compact version of the Translation Xaml extension like this:
            //
            // <Label Text="{ grial:Translate MyStringKey }" />
            //
            // Optionally:
            // <Label Text="{ grial:Translate Key=MyStringKey }" />
            //
            // To use another named Resx you can use either:
            // 
            // a) define the namspace of the Resx type, for instance:
            // 	  xmlns:resx="clr-namespace:Ubi"
            //
            //	  and use it like this:
            //	  <Label Text="{ grial:Translate Key=resx:OtherResources.MyStringKey }" />
            //
            //  b) define a StaticResource as an instance of the Resx type
            //	   <resx:OtherResources x:Key="MyOtherResourcesKey" />
            //
            //	   and use it like this:
            //	   <Label Text="{ grial:Translate Key=MyStringKey, Source={ StaticResource MyOtherResourcesKey } }" />
            //
            // Note: The Extension supports both Converter and StringFormat properties
            // as regular Bindings do. 

            Resources["DefaultStringResources"] = new Resx.AppResources();


            MainPage = GetMainPage();


            LinphoneWrapper.setNativeLogHandler();

            Core.SetLogLevelMask(0xFF);
            CoreListener listener = Factory.Instance.CreateCoreListener();
            listener.OnGlobalStateChanged = OnGlobal;
            LinphoneCore = Factory.Instance.CreateCore(listener, rc_path, null);
            LinphoneCore.NetworkReachable = true;
            // rEMOVIDO PARA PTESTE 
           // MainPage = new MainPage();


        }

        public StackLayout getLayoutView()
        {
            return MainPage.FindByName<StackLayout>("stack_layout");
        }

        private void OnGlobal(Core lc, GlobalState gstate, string message)
        {
            Console.WriteLine("Global state changed: " + gstate);

        }

        protected override void OnStart()
        {
            // Handle when your app starts
            Thread iterate = new Thread(LinphoneCoreIterate);
            iterate.IsBackground = false;
            iterate.Start();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void LinphoneCoreIterate()
        {
            while (true)
            {
                Device.BeginInvokeOnMainThread(() => { LinphoneCore.Iterate(); });
                Thread.Sleep(50);
            }
        }


    }

}


