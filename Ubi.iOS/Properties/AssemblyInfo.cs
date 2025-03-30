using System.Reflection;
using Xamarin.Forms;
using Ubi;
using UXDivers.Grial;

[assembly: AssemblyTitle(AssemblyGlobal.ProductLine + " - " + "Grial Xamarin.Forms UIKit (iOS)")]
[assembly: AssemblyConfiguration(AssemblyGlobal.Configuration)]
[assembly: AssemblyCompany(AssemblyGlobal.Company)]
[assembly: AssemblyProduct(AssemblyGlobal.ProductLine + " - " + "Grial Xamarin.Forms UIKit (iOS)")]
[assembly: AssemblyCopyright(AssemblyGlobal.Copyright)]

[assembly: UXDivers.Grial.GrialVersion("3.0.0.0")]

// Custom renderer definition
[assembly: ExportRenderer(typeof(EntryCell), typeof(UXDivers.Grial.EntryCellRenderer))]
[assembly: ExportRenderer(typeof(ImageCell), typeof(UXDivers.Grial.ImageCellRenderer))]
[assembly: ExportRenderer(typeof(SwitchCell), typeof(UXDivers.Grial.SwitchCellRenderer))]
[assembly: ExportRenderer(typeof(TableView), typeof(UXDivers.Grial.TableRenderer))]
[assembly: ExportRenderer(typeof(TextCell), typeof(UXDivers.Grial.TextCellRenderer))]
[assembly: ExportRenderer(typeof(ViewCell), typeof(UXDivers.Grial.ViewCellRenderer))]
[assembly: ExportRenderer(typeof(Entry), typeof(UXDivers.Grial.EntryRenderer))]
[assembly: ExportRenderer(typeof(Editor), typeof(UXDivers.Grial.EditorRenderer))]
[assembly: ExportRenderer(typeof(SearchBar), typeof(UXDivers.Grial.SearchBarRenderer))]
[assembly: ExportRenderer(typeof(Picker), typeof(UXDivers.Grial.PickerRenderer))]
[assembly: ExportRenderer(typeof(DatePicker), typeof(UXDivers.Grial.DatePickerRenderer))]
[assembly: ExportRenderer(typeof(TimePicker), typeof(UXDivers.Grial.TimePickerRenderer))]
[assembly: ExportRenderer(typeof(Page), typeof(UXDivers.Grial.PageRenderer))]
[assembly: ExportRenderer(typeof(CarouselPage), typeof(Ubi.iOS.CarouselPageVerticalScrollFixRenderer))]

[assembly: ExportRenderer(typeof(NavigationPage), typeof(UXDivers.Grial.GrialNavigationPageRenderer))]


#pragma warning disable 219
internal static class WorkaroundLoadingCustomRenderersFromExternalAssemblies
{
    static WorkaroundLoadingCustomRenderersFromExternalAssemblies()
    {
        var a = new UXDivers.Grial.EntryRenderer();
    }
}
#pragma warning restore 219
