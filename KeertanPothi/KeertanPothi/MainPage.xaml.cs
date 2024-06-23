using DBTest.Models;
using KeertanPothi.model;
using KeertanPothi.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KeertanPothi
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        int Taps = 0;
        //ObservableCollection<SideMenu> SideMenus = new ObservableCollection<SideMenu>();
        public MainPage()
        {
            VersionTracking.Track();
            Theme CurTheme = new Theme();
            InitializeComponent();
            string isDebug = string.Empty;
            #if DEBUG
                isDebug = "D";
            #endif
            lblVersion.Text = $"Version: {VersionTracking.CurrentVersion} (Db V: {Util.CurrentDbVersion}) {isDebug}";
            this.Detail = new NavigationPage(new Search())
            {
                BarBackgroundColor = Color.FromHex(CurTheme.HeaderColor),
                BarTextColor = Color.White,
            };
            LoadMenu();
            swtLanguage.IsToggled = Util.PrefSelectedLanguage == "P";
            bool isAdmin = Util.PrefIsAdmin;
            if (isAdmin)
                imgIsAdmin.Source = ImageSource.FromFile("unlock.png");
            else
                imgIsAdmin.Source = ImageSource.FromFile("Lock.png");
        }

        private void LoadMenu()
        {
            List<SideMenu> sideMenus = Util.GetStaticMenu().Where(a => a.IsVisible).ToList();
            lstMenu.ItemsSource = sideMenus;//SideMenus;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void lstMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            Theme CurTheme = new Theme();
            SideMenu itemClicked = e.SelectedItem as SideMenu;
            var type = Type.GetType("KeertanPothi.Views." + itemClicked.ViewName);
            Detail = new NavigationPage(Activator.CreateInstance(type) as Page)
            {
                BarBackgroundColor = Color.FromHex(CurTheme.HeaderColor),
                BarTextColor = Color.White,
            };
            IsPresented = false;
            lstMenu.SelectedItem = null;
        }

        private void Header_Tapped(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new DefaultDetail());
        }

        private void Language_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
                Util.PrefSelectedLanguage = "P";
            else
                Util.PrefSelectedLanguage = "E";
            LoadMenu();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if(Util.PrefIsAdmin == true)
                return;
            Taps += 1;
            if(Taps == 5)
            {
                string result = await DisplayPromptAsync("Admin Password?", "Please provide admin password to unlock admin mode");
                if (result == "wjkkwjkf")
                {
                    Util.PrefIsAdmin = true;
                    imgIsAdmin.Source = ImageSource.FromFile("unlock.png");
                }
            }
        }
    }
}
