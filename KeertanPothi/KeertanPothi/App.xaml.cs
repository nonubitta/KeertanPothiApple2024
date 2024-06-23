using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Threading.Tasks;
using DBTest.Models;

[assembly: ExportFont("gurbaniwebthick.ttf", Alias = "PunjabiBoldFont")]
[assembly: ExportFont("PunjabiFont1.otf", Alias = "PunjabiFont")]
[assembly: ExportFont("HandFont3.otf", Alias = "HandFont")]
[assembly: ExportFont("Gurhindi.ttf", Alias = "HindiFont")]
namespace KeertanPothi
{
    public partial class App : Application
    {
        public App()
        {
            DeviceDisplay.KeepScreenOn = true;
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzczMDA4QDMxMzgyZTM0MmUzME8raE9BVW8xVXlQV256bGZUNXJXV1BoaGRDSlFITWNsenUydjErandsQVE9");
            InitializeComponent();
            Device.SetFlags(new[]
            {
                "SwipeView_Experimental",
                "CarouselView_Experimental",
                "IndicatorView_Experimental",
                "RadioButton_Experimental",
                "Expander_Experimental"
            });

            if (!Preferences.Get(Util.PrefDataExistsKey, false))
            {
                try
                {
                    CopyDB.CopyDBFile(true);
                    Util.SetInitPreferences();
                }
                catch(Exception ex)
                {
                    Util.ShowRoast("First install error: " + ex.Message);
                    Util.Log("First install error(app.xaml.cs.1): \r\n" + ex.Message);
                }
            }
            else
            {
                try
                {
                    int appDbVersion = Util.PrefCurrentDbVersion;
                    //if (appDbVersion < Util.CurrentDbVersion)
                    //{

                        //Util.PrefCurrentDbVersion = Util.CurrentDbVersion;
                        //Util.RunScript();
                        Util.RunScriptProgressive();
                    //}
                }
                catch (Exception ex)
                {
                    Util.ShowRoast("Update install error: " + ex.Message);
                    Util.Log("Update install error(app.xaml.cs.2): \r\n" + ex.Message);
                }
            }
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
