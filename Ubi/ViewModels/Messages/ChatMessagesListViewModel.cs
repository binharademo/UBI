using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using UXDivers.Grial;
using Xamarin.Forms;

namespace Ubi
{
    public class ChatMessagesListViewModel : ObservableObject
    {
        private readonly string _variantPageName;

        public void Add(ChatMessageData cm)
        {
            Messages.Add(cm);
        }

        public ChatMessagesListViewModel(string variantPageName = null) : base(listenCultureChanges: true)
        {
            _variantPageName = variantPageName;
            LoadData();
            Messages = new ObservableCollection<ChatMessageData>();
            _itemCommand = new Command<NavigationItemData>(ItemAction);


        }

        //TODO: Não esta disparando esse comando
        private readonly Command _itemCommand;
        public ICommand ItemCommand => _itemCommand;
        private void ItemAction(NavigationItemData item)
        {
            // Action logic goes here...
            Application.Current.MainPage.DisplayAlert("Deu certo ?", "You have tapped a" , "OK");

        }

        public ObservableCollection<ChatMessageData> Messages { get; } = new ObservableCollection<ChatMessageData>();
        public ObservableCollection<NavigationItemData> Items { get; } = new ObservableCollection<NavigationItemData>();

        private NavigationCategoryData _category;

        public NavigationCategoryData Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        protected override void OnCultureChanged(CultureInfo culture)
        {
            LoadData();
        }

        private void LoadData()
        {
            //Messages.Clear();
            //JsonHelper.Instance.LoadViewModel(this, pageName: _variantPageName, source: "Messages.json");

            Category = null;
            Items.Clear();

            var i4 = new NavigationItemData();
            i4.Name = "Não";
            i4.BackgroundColor = "#FF0000"; //Vermelho
            i4.BackgroundImage = "https://s3-us-west-2.amazonaws.com/grial-images/v3.0/category_07.jpg";
            i4.Icon = "";
            i4.ItemCount = 15;
            i4.Description = "";
            i4.Badge = 0;
            Items.Add(i4);

            //var i2 = new NavigationItemData();
            //i2.Name = "Talvez";
            //i2.BackgroundColor = "#FFCC00";
            //i2.BackgroundImage = "https://s3-us-west-2.amazonaws.com/grial-images/v3.0/category_04.jpg";
            //i2.Icon = "?";
            //i2.ItemCount = 5;
            //i2.Description = "";
            //i2.Badge = 0;
            //Items.Add(i2);

            //var i2 = new NavigationItemData();
            //i2.Name = "";
            //i2.BackgroundColor = "#ffffff"; //branco
            //i2.BackgroundImage = "https://s3-us-west-2.amazonaws.com/grial-images/v3.0/category_05.jpg";
            //i2.Icon = "";
            //i2.ItemCount = 6;
            //i2.Description = "";
            //i2.Badge = 0;
            //Items.Add(i2);

            var i7 = new NavigationItemData();
            i7.Name = "Adicionar comentário";
            i7.BackgroundColor = "#F59F1D";
            i7.BackgroundImage = "https://s3-us-west-2.amazonaws.com/grial-images/v3.0/category_06.jpg";
            i7.Icon = "";
            i7.ItemCount = 5;
            i7.Description = "";
            i7.Badge = 0;
            Items.Add(i7);

            var i = new NavigationItemData();
            i.Name = "Sim";
            i.BackgroundColor = "#39B44A"; //Verde
            i.BackgroundImage = "https://s3-us-west-2.amazonaws.com/grial-images/v3.0/category_05.jpg";
            i.Icon = "";
            i.ItemCount = 6;
            i.Description = "";
            i.Badge = 0;
            Items.Add(i);

            var i5 = new NavigationItemData();
            //i5.Name = "Não se aplica";
            i5.Name = "Cancelar";
            i5.BackgroundColor = "#818181";
            i5.BackgroundImage = "https://s3-us-west-2.amazonaws.com/grial-images/v3.0/category_03.jpg";
            i5.Icon = "";
            i5.ItemCount = 10;
            i5.Description = "";
            i5.Badge = 0;
            Items.Add(i5);


            var i6 = new NavigationItemData();
            i6.Name = "Reagendar";
            i6.BackgroundColor = "#29C9CB"; //Azul
            i6.BackgroundImage = "https://s3-us-west-2.amazonaws.com/grial-images/v3.0/category_03.jpg";
            i6.Icon = "";
            i6.ItemCount = 7;
            i6.Description = "";
            i6.Badge = 0;
            Items.Add(i6);



            var i3 = new NavigationItemData();
            i3.Name = "Concluir Venda";
            i3.BackgroundColor = "#000000"; //preto
            i3.BackgroundImage = "https://s3-us-west-2.amazonaws.com/grial-images/v3.0/category_05.jpg";
            i3.Icon = "";
            i3.ItemCount = 6;
            i3.Description = "";
            i3.Badge = 0;
            Items.Add(i3);





            //JsonHelper.Instance.LoadViewModel(this, pageName: _variantPageName, source: "NavigationDashboards.json");
        }

        //private void Button_Onclicked()
        //{
        //    var mess = new ChatMessageData();
        //    ChatUserData userdata = new ChatUserData();
        //    userdata.Name = "UBI";
        //    userdata.Avatar = "icon.png";
        //    mess.From = userdata;
        //    //mess.Body = 
        //    mess.When = "Quando ? ";
        //    mess.IsRead = true;
        //    Messages.Add(mess);
        //}
    }
}
