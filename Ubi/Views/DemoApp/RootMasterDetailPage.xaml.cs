using System.Threading.Tasks;
using Xamarin.Forms;
using UXDivers.Grial;

namespace Ubi
{
    public partial class RootMasterDetailPage : MasterDetailPage
    {
        public RootMasterDetailPage()
        {
            InitializeComponent();

            Master = new MainMenuPage(LaunchPageInDetail);
        }

        private void LaunchPageInDetail(Page page)
        {
            Detail = page;
            IsPresented = false;
        }
    }
}