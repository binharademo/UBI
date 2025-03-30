using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

using UXDivers.Grial;

using Java.Util;
using Linphone;
using Org.Linphone.Mediastream.Video;

using Android.Content.Res;
using System.IO;


namespace Ubi.Droid
{
    //https://developer.android.com/guide/topics/manifest/activity-element.html
    [Activity(
        Label = "Ubi",
        Icon = "@drawable/icon",
        Theme = "@style/Theme.Splash",
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleTask,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Locale | ConfigChanges.LayoutDirection
        )
    ]
    public class MainActivity : FormsAppCompatActivity
    {
        #region Libliphone
        App application;
        Org.Linphone.Mediastream.Video.Display.GL2JNIView captureCamera;
        #endregion Libliphone

        private Locale _locale;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            #region Libliphone
            Java.Lang.JavaSystem.LoadLibrary("bctoolbox");
            Java.Lang.JavaSystem.LoadLibrary("ortp");
            Java.Lang.JavaSystem.LoadLibrary("mediastreamer_base");
            Java.Lang.JavaSystem.LoadLibrary("mediastreamer_voip");
            Java.Lang.JavaSystem.LoadLibrary("linphone");
            // This is mandatory for Android
            LinphoneAndroid.setAndroidContext(JNIEnv.Handle, this.Handle);
            #endregion Libliphone

            // Changing to App's theme since we are OnCreate and we are ready to 
            // "hide" the splash
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.AppTheme);

            FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabbar;

            base.OnCreate(savedInstanceState);

            // Initializing FFImageLoading
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: false);
            Forms.SetFlags("FastRenderers_Experimental");

            #region Libliphone
            AssetManager assets = Assets;
            string path = FilesDir.AbsolutePath;
            string rc_path = path + "/default_rc";
            using (var br = new BinaryReader(Android.App.Application.Context.Assets.Open("linphonerc_default")))
            {
                using (var bw = new BinaryWriter(new FileStream(rc_path, FileMode.Create)))
                {
                    byte[] buffer = new byte[2048];
                    int length = 0;
                    while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        bw.Write(buffer, 0, length);
                    }
                }
            }
            #endregion Libliphone


            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            // Initializing Xamarin.Essentials
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Initializing Popups
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            GrialKit.Init(this, "Ubi.Droid.GrialLicense");

            #region Libliphone
            application = new App(rc_path);
            captureCamera = new Org.Linphone.Mediastream.Video.Display.GL2JNIView(this);
            captureCamera.Holder.SetFixedSize(1920, 1080);
            AndroidVideoWindowImpl androidView = new AndroidVideoWindowImpl(captureCamera, null, null);
            application.LinphoneCore.NativeVideoWindowId = androidView.Handle;
            application.LinphoneCore.VideoDisplayEnabled = true;
            // removendo a chamada do stacklahout 
            //application.getLayoutView().Children.Add(captureCamera);
            LoadApplication(application);
            #endregion Libliphone

#if !DEBUG
            // Reminder to update the project license to production mode before publishing
            if (!UXDivers.Grial.License.IsProductionLicense())
            {
                try
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Grial UI Kit Reminder");
                    alert.SetMessage("Before publishing this App remember to change the license file to PRODUCTION MODE so it doesn't expire.");
                    alert.SetPositiveButton("OK", (sender, e) => { });

                    var dialog = alert.Create();
                    dialog.Show();
                }
                catch
                {
                }
            }
#endif

            _locale = Resources.Configuration.Locale;

            ReferenceCalendars();

#if GORILLA
            LoadApplication(
                UXDivers.Gorilla.Droid.Player.CreateApplication(
                    this,
                    new UXDivers.Gorilla.Config("Good Gorilla")
                        // Grial Core
                        .RegisterAssemblyFromType<UXDivers.Grial.NegateBooleanConverter>()

                        // // FFImageLoading.Transformations
                        .RegisterAssemblyFromType<FFImageLoading.Transformations.BlurredTransformation>()
                        // FFImageLoading.Forms
                        .RegisterAssemblyFromType<FFImageLoading.Forms.CachedImage>()

                        // Microcharts
                        .RegisterAssemblyFromType<Microcharts.ChartEntry>()
                        .RegisterAssemblyFromType<Microcharts.Forms.ChartView>()

                        // Grial Application .Net Standard
                        .RegisterAssembly(typeof(Ubi.App).Assembly)

                        .RegisterAssembly(typeof(Xamanimation.AnimationBase).Assembly)

                        .RegisterAssembly(typeof(AnimatedBaseBehavior).Assembly)
                    ));
#else

            //LoadApplication(new App());
#endif
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            GrialKit.NotifyConfigurationChanged(newConfig);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

#if GORILLA
        public override bool OnKeyUp (Android.Views.Keycode keyCode, Android.Views.KeyEvent e)  
        {  
            if ((keyCode == Android.Views.Keycode.F1 || keyCode == Android.Views.Keycode.Menu || keyCode == Android.Views.Keycode.VolumeUp || keyCode == Android.Views.Keycode.VolumeDown || keyCode == Android.Views.Keycode.VolumeMute) && UXDivers.Gorilla.Coordinator.Instance != null) {  
                UXDivers.Gorilla.ActionManager.ShowMenu();
                return true; 
            } 

            return base.OnKeyUp (keyCode, e); 
        }

        private readonly static GestureDetector LongPressDetector = new GestureDetector (new UXDivers.Gorilla.Droid.LongPressGestureListener());

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            LongPressDetector.OnTouchEvent(ev);
                             
            return base.DispatchTouchEvent(ev);
        }
#endif

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

