using UXDivers.Grial;
namespace Ubi
{
    public partial class WalkthroughMinimalPage : WalkthroughBasePage
    {
        public WalkthroughMinimalPage()
        {
            InitializeComponent();

            BindingContext = new WalkthroughViewModel(Close, MoveNext);
        }
    }
}