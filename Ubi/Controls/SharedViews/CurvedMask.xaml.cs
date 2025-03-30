using System;
using System.Collections.Generic;
using FFImageLoading.Svg.Forms;
using FFImageLoading.Transformations;
using Xamarin.Forms;
using UXDivers.Grial;

namespace Ubi
{
    public partial class CurvedMask : ContentView
    {
        public static BindableProperty MaskColorProperty =
            BindableProperty.Create(
                nameof(MaskColor),
                typeof(Color),
                typeof(CurvedMask),
                defaultValue: Color.Default,
                propertyChanged: OnMaskColorChanged
            );

        public Color MaskColor
        {
            get { return (Color)GetValue(MaskColorProperty); }
            set { SetValue(MaskColorProperty, value); }
        }

        public CurvedMask()
        {
            InitializeComponent();
        }

        private static void OnMaskColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CurvedMask)bindable).Update();
        }

        private void Update()
        {
            root.Children.Clear();

            if (MaskColor != Color.Default)
            {
                var image = new SvgCachedImage
                {
                    Style = Resources["MaskImageStyle"] as Style
                };

                image.Transformations.Add(new TintTransformation(MaskColor.ToHex()) { EnableSolidColor = true });
                root.Children.Add(image);
            }
        }
    }
}
