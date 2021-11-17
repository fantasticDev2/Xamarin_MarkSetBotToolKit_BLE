using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace MarkSetBot_ToolKit.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        // special method called by shiny if exists
        partial void OnPreFinishedLaunching(UIApplication app, NSDictionary options);

        // special method called by shiny if exists
        partial void OnPostFinishedLaunching(UIApplication app, NSDictionary options);
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            this.OnPreFinishedLaunching(app, options);

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            UINavigationBar.Appearance.Translucent = false;

            global::XF.Material.iOS.Material.Init();
            this.OnPostFinishedLaunching(app, options);

            return base.FinishedLaunching(app, options);
        }
    }
}
