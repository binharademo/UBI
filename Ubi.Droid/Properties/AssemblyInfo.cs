using System.Reflection;
using Xamarin.Forms;
using Ubi;
using UXDivers.Grial;

[assembly: AssemblyTitle(AssemblyGlobal.ProductLine + " - " + "Grial Xamarin.Forms UIKit (Android)")]
[assembly: AssemblyConfiguration(AssemblyGlobal.Configuration)]
[assembly: AssemblyCompany(AssemblyGlobal.Company)]
[assembly: AssemblyProduct(AssemblyGlobal.ProductLine + " - " + "Grial Xamarin.Forms UIKit (Android)")]
[assembly: AssemblyCopyright(AssemblyGlobal.Copyright)]

[assembly: UXDivers.Grial.GrialVersion("3.0.0.0")]

// Custom renderer definition.

[assembly: ExportRenderer(typeof(Entry), typeof(UXDivers.Grial.EntryRenderer))]
[assembly: ExportRenderer(typeof(Editor), typeof(UXDivers.Grial.EditorRenderer))]
[assembly: ExportRenderer(typeof(Switch), typeof(UXDivers.Grial.SwitchRenderer))]
[assembly: ExportRenderer(typeof(ActivityIndicator), typeof(UXDivers.Grial.ActivityIndicatorRenderer))]
[assembly: ExportRenderer(typeof(SwitchCell), typeof(UXDivers.Grial.SwitchCellRenderer))]
[assembly: ExportRenderer(typeof(TextCell), typeof(UXDivers.Grial.TextCellRenderer))]
[assembly: ExportRenderer(typeof(ImageCell), typeof(UXDivers.Grial.ImageCellRenderer))]
[assembly: ExportRenderer(typeof(ViewCell), typeof(UXDivers.Grial.ViewCellRenderer))]
[assembly: ExportRenderer(typeof(EntryCell), typeof(UXDivers.Grial.EntryCellRenderer))]
[assembly: ExportRenderer(typeof(SearchBar), typeof(UXDivers.Grial.SearchBarRenderer))]
[assembly: ExportRenderer(typeof(DatePicker), typeof(UXDivers.Grial.DatePickerRenderer))]
[assembly: ExportRenderer(typeof(TimePicker), typeof(UXDivers.Grial.TimePickerRenderer))]
[assembly: ExportRenderer(typeof(ExtendedCarouselViewControl), typeof(Ubi.Droid.ExtendedCarouselViewRenderer))]

[assembly: ExportRenderer(typeof(NavigationPage), typeof(UXDivers.Grial.GrialNavigationPageRenderer))]
[assembly: ExportRenderer(typeof(Picker), typeof(UXDivers.Grial.PickerRenderer))]

// Fix for ScrollView layout after device orientation change
[assembly: ExportRenderer(typeof(ScrollView), typeof(Ubi.Droid.ScrollViewRendererOrientationFix))]

// XF 3.1+ Includes properties to control the colors of sliders that work on Android API level >= 21.
// Uncomment below renderer if you are targeting Android API <= 20 to get the Grial theme accent color applied to the Slider.
// Note that this renderer owerrides the beahvior of the Slider color properties of XF 3.1+. If you need to set a specific color
// you can use grial:SliderProperties.TintColor attached property.
//[assembly: ExportRenderer(typeof(Slider), typeof(UXDivers.Grial.SliderRenderer))]