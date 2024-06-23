using System;
using Foundation;
using KeertanPothi.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(SetStatusBarStyle))]
namespace KeertanPothi.iOS
{
    public class SetStatusBarStyle : ISetStatusBarStyle
    {
        public void SetStatusBarColor(System.Drawing.Color color)
        {
            UIView statusBar = UIApplication.SharedApplication.ValueForKey(
            new NSString("statusBar")) as UIView;

            if (statusBar != null && statusBar.RespondsToSelector(
            new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                // change to your desired color 
                statusBar.BackgroundColor = Color.FromHex("#7f6550").ToUIColor();
            }
        }
    }
}




