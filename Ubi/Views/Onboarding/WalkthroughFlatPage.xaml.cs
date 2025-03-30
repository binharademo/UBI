using System.Threading.Tasks;
using UbiModelShared.Connection;
//using UbiModel.Config1;
using UXDivers.Grial;


namespace Ubi
{
    public partial class WalkthroughFlatPage : WalkthroughBasePage
    {
        public WalkthroughFlatPage()
        {
            InitializeComponent();

            BindingContext = new WalkthroughViewModel(Close, MoveNext);
            //User1 u = new User1();
            //u.Name = "Jose";
            //u.Cpf = "02562691976";
            /*ConectionRealm ConnReaml = ConectionRealm.getInstance();

             ConnReaml.AddEntry("Binhara");*/ //====> ESSE TRECHO FAZIA DAR THROW EXCEPTION NO TRY_CATCH
        }

        public async Task MoveNext() {
            App.Current.MainPage = new WalkthroughImagePage();
        }

        public async Task Close() {
            App.Current.MainPage = new TabbedLoginPage();
        }

    }
}

