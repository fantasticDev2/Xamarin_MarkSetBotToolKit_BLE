using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Shiny;
using Android.Content;

namespace MarkSetBot_ToolKit.Android
{
    [global::Android.App.ApplicationAttribute]
    public partial class MainApplication : global::Android.App.Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) { }

        public override void OnCreate()
        {
            this.ShinyOnCreate(new MarkSetBot_ToolKit.MarkSetBotStartup());
            global::Xamarin.Essentials.Platform.Init(this);
            base.OnCreate();
        }
    }
}

namespace MarkSetBot_ToolKit.Droid
{
    [Activity(Label = "MarkSetBot_ToolKit", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity: global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //partial void OnPreCreate(Bundle savedInstanceState);
        //partial void OnPostCreate(Bundle savedInstanceState);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            this.ShinyOnCreate();
            //this.OnPreCreate(savedInstanceState);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            this.ShinyOnNewIntent(intent);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            this.ShinyOnActivityResult(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            this.ShinyOnRequestPermissionsResult(requestCode, permissions, grantResults);
            global::Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /*
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        */
    }
}