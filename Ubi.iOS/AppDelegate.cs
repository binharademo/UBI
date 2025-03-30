using Foundation;
using UIKit;
using Xamarin.Forms;
using FFImageLoading.Forms.Platform;
using CarouselView.FormsPlugin.iOS;
using UXDivers.Grial;
using System;
using FFImageLoading.Svg.Forms;
using UXDivers.Grial;


namespace Ubi.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();

            // This line enables the device to enter sleep mode, it should be false by default
            // but without this explicit assignment it never sleeps with latest Xamarin.Forms.
            // Set it to true if you need to prevent the device to enter sleep mode while displaying the App 
            UIApplication.SharedApplication.IdleTimerDisabled = false;

            var ignore = typeof(SvgCachedImage);
            CachedImageRenderer.Init(); // Initializing FFImageLoading

            CarouselViewRenderer.Init(); // Initializing CarouselView

            Rg.Plugins.Popup.Popup.Init();

            GrialKit.Init(new ThemeColors(), "Ubi.iOS.GrialLicense");

#if !DEBUG
            // Reminder to update the project license to production mode before publishing
            if (!UXDivers.Grial.License.IsProductionLicense())
            {
                BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        var alert = UIAlertController.Create(
                            "Grial UI Kit Reminder",
                            "Before publishing this App remember to change the license file to PRODUCTION MODE so it doesn't expire.",
                            UIAlertControllerStyle.Alert);

                        alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));

                        var root = UIApplication.SharedApplication.KeyWindow.RootViewController;
                        var controller = root.PresentedViewController ?? root.PresentationController.PresentedViewController;
                        controller.PresentViewController(alert, animated: true, completionHandler: null);
                    }
                    catch
                    {
                    }
                });
            }
#endif

            // Code for starting up the Xamarin Test Cloud Agent
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif

            FormsHelper.ForceLoadingAssemblyContainingType<FFImageLoading.Transformations.BlurredTransformation>();

            ReferenceCalendars();

#if GORILLA
            LoadApplication(
                UXDivers.Gorilla.iOS.Player.CreateApplication(
                    new UXDivers.Gorilla.Config("Good Gorilla")
                        // Grial Core
                        .RegisterAssemblyFromType<UXDivers.Grial.NegateBooleanConverter>()

                        // FFImageLoading.Forms
                        .RegisterAssemblyFromType<FFImageLoading.Forms.CachedImage>()
                        // FFImageLoading.Transformations
                        .RegisterAssemblyFromType<FFImageLoading.Transformations.BlurredTransformation>()
                        // FFImageLoading.Svg.Forms
                        .RegisterAssemblyFromType<FFImageLoading.Svg.Forms.SvgCachedImage>()

                        // Microcharts
                        .RegisterAssemblyFromType<Microcharts.ChartEntry>()
                        .RegisterAssemblyFromType<Microcharts.Forms.ChartView>()

                        // Grial Application .NET Standard
                        .RegisterAssembly(typeof(Ubi.App).Assembly)

            			.RegisterAssembly(typeof(CarouselViewRenderer).Assembly)
            			.RegisterAssembly(typeof(CarouselView.FormsPlugin.Abstractions.CarouselViewControl).Assembly)

                        .RegisterAssembly(typeof(Rg.Plugins.Popup.Pages.PopupPage).Assembly)

                        .RegisterAssembly(typeof(Xamanimation.AnimationBase).Assembly)
                        .RegisterAssembly(typeof(AnimatedBaseBehavior).Assembly)
                ));
#else
            LoadApplication(new App());
#endif

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        private void ReferenceCalendars()
        {
            // When compiling in release, you may need to instantiate the specific
            // calendar so it doesn't get stripped out by the linker. Just uncomment
            // the lines you need according to the localization needs of the app.
            // For instance, in 'ar' cultures UmAlQuraCalendar is required.
            // https://bugzilla.xamarin.com/show_bug.cgi?id=59077

            new System.Globalization.UmAlQuraCalendar();
            // new System.Globalization.ChineseLunisolarCalendar();
            // new System.Globalization.ChineseLunisolarCalendar();
            // new System.Globalization.HebrewCalendar();
            // new System.Globalization.HijriCalendar();
            // new System.Globalization.IdnMapping();
            // new System.Globalization.JapaneseCalendar();
            // new System.Globalization.JapaneseLunisolarCalendar();
            // new System.Globalization.JulianCalendar();
            // new System.Globalization.KoreanCalendar();
            // new System.Globalization.KoreanLunisolarCalendar();
            // new System.Globalization.PersianCalendar();
            // new System.Globalization.TaiwanCalendar();
            // new System.Globalization.TaiwanLunisolarCalendar();
            // new System.Globalization.ThaiBuddhistCalendar();
        }
    }
}
