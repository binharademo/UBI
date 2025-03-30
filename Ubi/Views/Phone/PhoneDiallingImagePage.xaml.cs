using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ubi.Views.Phone
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhoneDiallingImagePage : ContentPage
    {
        public PhoneDiallingImagePage()
        {
            InitializeComponent();

            var numEntry = new Entry()
            {
                Placeholder = "Insira número",
                Keyboard = Keyboard.Numeric,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 20




            };
            StackPhoneImage.Children.Add(numEntry);

            var label = new Label
            {
                Text = "Contato Recente",
                TextColor = Color.Gray,
                TextDecorations = TextDecorations.Underline,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center


            };
            StackPhoneImage.Children.Add(label);
        }
    }
}