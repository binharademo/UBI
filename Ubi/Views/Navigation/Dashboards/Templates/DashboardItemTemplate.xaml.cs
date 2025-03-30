using Xamarin.Forms;
using UXDivers.Grial;
using System;

namespace Ubi
{
    public partial class DashboardItemTemplate : DashboardItemTemplateBase
    {
        public DashboardItemTemplate()
        {
            InitializeComponent();
        }

        protected override void OnTapped(object sender, EventArgs e)
        {
            try
            {
                //BoxView v = (BoxView)sender;
                NavigationItemData sourcedata = (NavigationItemData)box.BindingContext;
                //Application.Current.MainPage.DisplayAlert("Item Tapped! binhara", "You have tapped a" + sourcedata.Name, "OK");

                if (sourcedata.BackgroundColor.Equals("#39B44A")) {//sim
                    ChatTimelinePage.getInstance().BtYes_OnClicked(sender, e);
                }
                if (sourcedata.BackgroundColor.Equals("#FF0000")) {//nao
                    ChatTimelinePage.getInstance().BtNo_OnClicked(sender, e);
                }
                if (sourcedata.BackgroundColor.Equals("#818181")) {//cancelar
                    ChatTimelinePage.getInstance().BtCancel_OnClicked(sender, e);
                }
                if (sourcedata.BackgroundColor.Equals("#29C9CB")) {//reagendar
                    ChatTimelinePage.getInstance().Bt_OpenPopup_Reschedule(sender, e);
                }
                if (sourcedata.BackgroundColor.Equals("#000000")) {//Concluir Venda 
                    ChatTimelinePage.getInstance().BtVender_OnClicked(sender, e);
                }
                if (sourcedata.BackgroundColor.Equals("#F59F1D")) {//adicionar comentario
                    ChatTimelinePage.getInstance().OpenCommentModal(sender, e);
                }

            }
            catch (Exception eee)
            {

            }
        }
    }
}