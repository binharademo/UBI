using Xamarin.Forms;
using UXDivers.Grial;
namespace Ubi
{
    public partial class App : Application
    {
        public App()
        {
            instance = this;
            InitializeComponent();
            #region
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
            #endregion
            Resources["DefaultStringResources"] = new Resx.AppResources();
            MainPage = GetMainPage();
        }

        private static App instance;

        public static App getInstance()
        {
            return instance;
        }

        public static Page GetMainPage() {
            return new WalkthroughFlatPage();
        }
    }
}
