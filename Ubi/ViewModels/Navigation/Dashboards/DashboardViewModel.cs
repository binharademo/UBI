using System.Collections.ObjectModel;
using System.Globalization;
using UbiModelShared.Mock;
using UbiModelShared.Poco;

namespace Ubi.ViewModels.Navigation.Dashboards
{
   public class DashboardViewModel : ObservableObject
    {
        public DashboardViewModel() : base(listenCultureChanges: true)
        {
            LoadData();
        }

        public ObservableCollection<Points> ItemPontos { get; } = new ObservableCollection<Points>();

        protected override void OnCultureChanged(CultureInfo culture)
        {
            LoadData();
        }
        private void LoadData()
        {
            ItemPontos.Clear();

            JsonHelper.Instance.LoadViewModel(this, source: "NavigationDashboards.json");

            var p = new MockPontos().ListaP();
            foreach (var i in p)
            {
                ItemPontos.Add(i);
            }
        
        }
    }
}
