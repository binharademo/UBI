using System;
using Xamarin.Forms;
using UXDivers.Grial;

namespace Ubi
{
    public partial class EcommerceNavigationPage
    {
        public EcommerceNavigationPage()
        {
        }

        public EcommerceNavigationPage(Page root)
            : base(root)
        {
            InitializeComponent();
        }

        private void OnClose(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
