using System;
using Xamarin.Forms;
using UXDivers.Grial;

namespace Ubi
{
    public class WalkthroughBaseStepItemTemplate : ContentPage
    {
        public event EventHandler ItemAppearing;
        public event EventHandler ItemDisappearing;
        public event EventHandler ItemInitialized;

        public void Initialize()
        {
            ItemInitialized?.Invoke(this, EventArgs.Empty);
        }

        public void ItemAppear()
        {
            ItemAppearing?.Invoke(this, EventArgs.Empty);
        }

        public void ItemDisappear()
        {
            ItemDisappearing?.Invoke(this, EventArgs.Empty);
        }

        /* TEXT */

        public static BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(WalkthroughBaseStepItemTemplate),
                string.Empty
            );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /* HEADER */

        public static BindableProperty HeaderProperty =
            BindableProperty.Create(
                nameof(Header),
                typeof(string),
                typeof(WalkthroughBaseStepItemTemplate),
                string.Empty
            );

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /* BUTTON */

        public static BindableProperty ButtonTextProperty =
            BindableProperty.Create(
                nameof(ButtonText),
                typeof(string),
                typeof(WalkthroughBaseStepItemTemplate),
                string.Empty
            );

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public static BindableProperty ButtonBackgroundColorProperty =
            BindableProperty.Create(
                nameof(ButtonBackgroundColor),
                typeof(Color),
                typeof(WalkthroughBaseStepItemTemplate),
                Color.Default
            );

        public Color ButtonBackgroundColor
        {
            get { return (Color)GetValue(ButtonBackgroundColorProperty); }
            set { SetValue(ButtonBackgroundColorProperty, value); }
        }

        /* ICON */

        public static BindableProperty IconColorProperty =
            BindableProperty.Create(
                nameof(IconColor),
                typeof(Color),
                typeof(WalkthroughBaseStepItemTemplate),
                Color.Default
            );

        public Color IconColor
        {
            get { return (Color)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        /* IMAGE */

        public static BindableProperty StepBackgroundImageProperty =
            BindableProperty.Create(
                nameof(StepBackgroundImage),
                typeof(string),
                typeof(WalkthroughBaseStepItemTemplate),
                string.Empty
            );

        public string StepBackgroundImage
        {
            get { return (string)GetValue(StepBackgroundImageProperty); }
            set { SetValue(StepBackgroundImageProperty, value); }
        }

        public static BindableProperty IconTextProperty =
            BindableProperty.Create(
                nameof(IconText),
                typeof(string),
                typeof(WalkthroughBaseStepItemTemplate),
                string.Empty
            );

        public string IconText
        {
            get { return (string)GetValue(IconTextProperty); }
            set { SetValue(IconTextProperty, value); }
        }
    }
}

