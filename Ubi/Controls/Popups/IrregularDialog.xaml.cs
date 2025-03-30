using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using UXDivers.Grial;

namespace Ubi
{
    public partial class IrregularDialog : PopupPage
    {
        public IrregularDialog()
        {
            InitializeComponent();
        }


        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}
