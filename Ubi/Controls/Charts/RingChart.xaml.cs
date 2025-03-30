using System;
using System.Collections.Generic;

using Xamarin.Forms;
using UXDivers.Grial;

namespace Ubi
{
    public partial class RingChart : ContentView
    {
        public RingChart()
        {
            InitializeComponent();
        }

        // IsAnimated
        public static readonly BindableProperty IsAnimatedProperty = 
            BindableProperty.Create(
                nameof(IsAnimated), 
                typeof(bool), 
                typeof(RingChart), 
                false,
                defaultBindingMode: BindingMode.OneWay
            );

        public bool IsAnimated
        {
            get { return (bool)GetValue(IsAnimatedProperty); }
            set { SetValue(IsAnimatedProperty, value); }
        }

        public static BindableProperty RingChartTextColorProperty =
            BindableProperty.Create(
                nameof(RingChartTextColor),
                typeof(Color),
                typeof(RingChart),
                defaultValue: Color.Black,
                defaultBindingMode: BindingMode.OneWay
            );

        public Color RingChartTextColor
        {
            get { return (Color)GetValue(RingChartTextColorProperty); }
            set { SetValue(RingChartTextColorProperty, value); }
        }

        public static BindableProperty RingChartLabelFontSizeProperty =
            BindableProperty.Create(
                nameof(RingChartLabelFontSize),
                typeof(Double),
                typeof(RingChart),
                defaultValue: 10.0,
                defaultBindingMode: BindingMode.OneWay
            );

        public Double RingChartLabelFontSize
        {
            get { return (Double)GetValue(RingChartLabelFontSizeProperty); }
            set { SetValue(RingChartLabelFontSizeProperty, value); }
        }

        public static BindableProperty RingChartValueLabelFontSizeProperty =
            BindableProperty.Create(
                nameof(RingChartValueLabelFontSize),
                typeof(Double),
                typeof(RingChart),
                defaultValue: 10.0,
                defaultBindingMode: BindingMode.OneWay
            );

        public Double RingChartValueLabelFontSize
        {
            get { return (Double)GetValue(RingChartValueLabelFontSizeProperty); }
            set { SetValue(RingChartValueLabelFontSizeProperty, value); }
        }

        public static BindableProperty RingChartValueLabelProperty =
            BindableProperty.Create(
                nameof(RingChartValueLabel),
                typeof(string),
                typeof(RingChart),
                defaultValue: "",
                defaultBindingMode: BindingMode.OneWay
            );

        public string RingChartValueLabel
        {
            get { return (string)GetValue(RingChartValueLabelProperty); }
            set { SetValue(RingChartValueLabelProperty, value); }
        }

        public static BindableProperty RingChartLabelProperty =
            BindableProperty.Create(
                nameof(RingChartLabel),
                typeof(string),
                typeof(RingChart),
                defaultValue: "",
                defaultBindingMode: BindingMode.OneWay
            );

        public string RingChartLabel
        {
            get { return (string)GetValue(RingChartLabelProperty); }
            set { SetValue(RingChartLabelProperty, value); }
        }

        public static BindableProperty ValueProperty =
            BindableProperty.Create(
                nameof(Value),
                typeof(double),
                typeof(RingChart),
                defaultValue: 0.0,
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: OnValueChanged
            );

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static BindableProperty ValueColorProperty =
            BindableProperty.Create(
                nameof(ValueColor),
                typeof(Color),
                typeof(RingChart),
                defaultValue: Color.Default,
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: OnColorChanged
            );

        public Color ValueColor
        {
            get { return (Color)GetValue(ValueColorProperty); }
            set { SetValue(ValueColorProperty, value); }
        }

        private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((RingChart)bindable).Update();
        }

        private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((RingChart)bindable).Update();
        }

        private void Update()
        {
            if (ValueColor == Color.Default)
            {
                return;
            }

            chart.IsAnimated = IsAnimated;

            var lightColor = new Color(ValueColor.R, ValueColor.G, ValueColor.B, 0.35);
            var value = (float)Value;
            chart.ItemsSource = new Microcharts.ChartEntry[]
            {
                new Microcharts.ChartEntry(value) 
                {
                    Color = SkiaSharp.SKColor.Parse(ValueColor.ToHex()) 
                },
                new Microcharts.ChartEntry(100 - value) 
                {
                    Color = SkiaSharp.SKColor.Parse(lightColor.ToHex()) 
                }
            };
        }
    }
}
