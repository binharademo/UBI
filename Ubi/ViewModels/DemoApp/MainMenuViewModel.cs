using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using UXDivers.Grial;
using Ubi.Views.Navigation.Dashboards;
using Ubi.Views.DemoApp;
using Ubi.Views.Phone;

namespace Ubi {
    public class MainMenuViewModel : ObservableObject {
        private readonly INavigation _navigation;
        private readonly Action<Page> _openPageAsRoot;
        private List<MenuEntry> _mainMenuEntries;
        private MenuEntry _selectedMainMenuEntry;

        public MainMenuViewModel(INavigation navigation, Action<Page> openPageAsRoot)
            : base(listenCultureChanges: true)
        {
            _navigation = navigation;
            _openPageAsRoot = openPageAsRoot;

            LoadData();

            var firstEntry = _mainMenuEntries[0];
            if (firstEntry.IsModal) {
                Page page = new HomePage();
                _openPageAsRoot(new NavigationPage(page));
                _selectedMainMenuEntry = null;

            } else {
                MainMenuSelectedItem = firstEntry;
            }
        }

        public List<MenuEntry> MainMenuEntries {
            get { return _mainMenuEntries; }
            set { SetProperty(ref _mainMenuEntries, value); }
        }

        public MenuEntry MainMenuSelectedItem {
            get { return _selectedMainMenuEntry; }
            set {
                if (SetProperty(ref _selectedMainMenuEntry, value) && value != null) {
                    Page page;

                    if (value.PageType != null) {
                        page = CreatePage(value.PageType);
                    } else {
                        page = value.CreatePage();
                    }

                    NavigationPage navigationPage;

                    if (value.NavigationPageType == null) {
                        navigationPage = new NavigationPage(page);
                    } else {
                        navigationPage = (NavigationPage)Activator.CreateInstance(value.NavigationPageType, page);
                    }

                    if (value.UseTransparentNavBar) {
                        GrialNavigationPage.SetIsBarTransparent(navigationPage, true);
                    }

                    if (_selectedMainMenuEntry.IsModal) {
                        _navigation.PushModalAsync(navigationPage);
                    } else {
                        _openPageAsRoot(navigationPage);
                    }

                    _selectedMainMenuEntry = null;
                    NotifyPropertyChanged(nameof(MainMenuSelectedItem));
                }
            }
        }

        protected override void OnCultureChanged(CultureInfo culture) {
            LoadData();
        }

        private void LoadData() {
            MainMenuEntries = new List<MenuEntry>
            {
               //new MenuEntry { Name = "Saiba como funciona", Icon = GrialIconsFont.Book, PageType = typeof(WalkthroughImagePage), IsModal = true }, 
               //new MenuEntry { Name = Resx.AppResources.PageTitleThemeOverview, Icon = GrialIconsFont.Fire, PageType = typeof(ThemeOverviewPage), IsModal = false }, 
               //new MenuEntry { Name = "Página UBI", Icon = GrialIconsFont.Fire, PageType = typeof(WalkthroughFlatPage), IsModal = true }, 

               //new MenuEntry { Name = Resx.AppResources.PageTitleCheckout, Icon = GrialIconsFont.Fire, PageType = typeof(CheckoutPage), IsModal = false, NavigationPageType = typeof(EcommerceNavigationPage) },
             new MenuEntry { Name = "Home", Icon = GrialIconsFont.Home, PageType = typeof(HomePage), IsModal = false },
             ////new MenuEntry { Name = "Capacitação", Icon = GrialIconsFont.UserPlus, PageType = typeof(DashboardMultipleTilesPage), IsModal = false },
             //new MenuEntry { Name = "Dashboard", Icon = GrialIconsFont.Dashboard, PageType = typeof(DashboardPage), IsModal = false },
             //new MenuEntry { Name = Resx.AppResources.PageTitleSettings, Icon = GrialIconsFont.Fire, PageType = typeof(SettingsPage), IsModal = false },
             //new MenuEntry { Name = "Trocar de Conta", Icon = GrialIconsFont.LogIn, PageType = typeof(TabbedLoginPage), IsModal = true },
             ////extra?             
             //new MenuEntry { Name = "Call Voip", Icon = GrialIconsFont.Fire, PageType = typeof(CallVoipPage), IsModal = false },
             //new MenuEntry { Name = "Menu Voip", Icon = GrialIconsFont.Fire, PageType = typeof(MenuVoipPage), IsModal = false },
             //new MenuEntry { Name = "Test Voip", Icon = GrialIconsFont.Fire, PageType = typeof(UbuPhoneTestPage), IsModal = false },
             //new MenuEntry { Name = "Perfil", Icon = GrialIconsFont.User, PageType = typeof(ProfilePage), IsModal = false },
             //new MenuEntry { Name = "Etapa Concluída", Icon = GrialIconsFont.Fire, PageType = typeof(WalkthroughMinimalPage), IsModal = true },
             new MenuEntry { Name = "Atendimento", Icon = GrialIconsFont.MessageSquare, PageType = typeof(ChatTimelinePage), IsModal = false },
             //new MenuEntry { Name = "Menu Atendimento", Icon = GrialIconsFont.Fire, PageType = typeof(FlatDashboardPage), IsModal = false },
             ////new MenuEntry { Name = "Curso Básico", Icon = GrialIconsFont.UserPlus, PageType = typeof(CursesBasicPage), IsModal = false },
             // new MenuEntry { Name = "Phone Dialler", Icon = GrialIconsFont.Fire, PageType = typeof(PhoneDiallingPage), IsModal = false },
             // new MenuEntry { Name = "Dialler Image", Icon = GrialIconsFont.Fire, PageType = typeof(PhoneDiallingImagePage), IsModal = false }

                };
        }

        private Page CreatePage(Type pageType) {
            return Activator.CreateInstance(pageType) as Page;
        }

        private static ContentPage CreateDetailDefaultBackgroundPage() {
            var content = new Grid();
            var logo = new Label {
                Text = GrialIconsFont.LogoAndroid,
                FontSize = 100,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Style = (Style)Application.Current.Resources["FontIcon"]
            };
            logo.SetDynamicResource(Label.TextColorProperty, "ComplementColor");
            content.Children.Add(logo);
            return new ContentPage { Content = content };
        }

        public class MenuEntry {
            public string Name { get; set; }
            public string Icon { get; set; }
            public bool UseTransparentNavBar { get; set; }
            public Type PageType { get; set; }
            public Func<Page> CreatePage { get; set; }
            public Type NavigationPageType { get; set; }
            public bool IsModal { get; set; }
        }
    }

}