using System.Threading.Tasks;
using UXDivers.Grial;
namespace Ubi
{
    public partial class WalkthroughImagePage : WalkthroughBasePage
    {
        public WalkthroughImagePage()
        {
            InitializeComponent();

            BindingContext = new WalkthroughViewModel(Close, MoveNext);
        }

        public async Task MoveNext() {
            App.Current.MainPage = new TabbedLoginPage();
        }

        public async Task Close() {
            App.Current.MainPage = new TabbedLoginPage();
        }

    }
}
