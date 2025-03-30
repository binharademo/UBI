using System.Collections.ObjectModel;
using System.Globalization;
using UbiModelShared.Poco;
using UXDivers.Grial;

namespace Ubi
{
    public class DashboardMultipleTilesViewModel : ObservableObject
    {
        public DashboardMultipleTilesViewModel() : base(listenCultureChanges: true)
        {
            LoadData();
        }

        //public ObservableCollection<DashboardMultipleTileItemData> _items { get; } = new ObservableCollection<DashboardMultipleTileItemData>();
        public ObservableCollection<Categoria> Items { get; } = new ObservableCollection<Categoria>();


        protected override void OnCultureChanged(CultureInfo culture)
        {
            LoadData();
        }

        private void LoadData()
        {
            Items.Clear();

            JsonHelper.Instance.LoadViewModel(this, source: "NavigationDashboards.json");
             
            var l = new MockCategoria().Lista();
            foreach (var i in l)
            {
                Items.Add(i);
            }
        }
    }
}