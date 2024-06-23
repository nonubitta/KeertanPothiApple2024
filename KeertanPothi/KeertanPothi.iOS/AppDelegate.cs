using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.XForms.iOS.EffectsView;
using Syncfusion.XForms.iOS.PopupLayout;

namespace KeertanPothi.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            Rg.Plugins.Popup.Popup.Init();
            //Acr.UserDialogs.UserDialogs.Init();
            global::Xamarin.Forms.Forms.Init();
            SfListViewRenderer.Init();
            SfEffectsViewRenderer.Init();
            SfPopupLayoutRenderer.Init();
            LoadApplication(new App());
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(25, 118, 210);
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes { TextColor = UIColor.White });
            return base.FinishedLaunching(app, options);
        }
    }
}
