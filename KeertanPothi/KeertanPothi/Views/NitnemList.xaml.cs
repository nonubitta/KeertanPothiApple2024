using Acr.UserDialogs;
using DBTest.Models;
using Rg.Plugins.Popup.Extensions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NitnemList : ContentPage
    {

        SQLiteAsyncConnection _con;
        ObservableCollection<NitnemBani> nitnemListOrb = new ObservableCollection<NitnemBani>();
        public NitnemList()
        {
            BindingContext = new Theme();
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            InitializeComponent();
            LoadBaniList();
            MessagingCenter.Subscribe<App>((App)Application.Current, "BaniListUpdated", (sender) => {
                OnListUpdated();
            });
        }
        protected override void OnAppearing()
        {
            Theme theme = new Theme();
            nitnemListOrb?.ToList().ForEach(a => a.PageBgTheme = theme);
            BindingContext = theme;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<App, object>(this, "BaniListUpdated");
        }

        protected void OnListUpdated()
        {
            LoadBaniList();
        }

        private async void LoadBaniList()
        {
            List<NitnemBani> banis = await _con.QueryAsync<NitnemBani>(Queries.GetAllBanis(false));
            if (banis.Count > 0)
            {
                nitnemListOrb = new ObservableCollection<NitnemBani>(banis);
                lstNitnem.ItemsSource = nitnemListOrb;
                lblNoBani.IsVisible = false;
                lstNitnem.IsVisible = true;
            }
            else
            {
                lblNoBani.IsVisible = true;
                lstNitnem.IsVisible = false;
            }

        }

        private void lstNitnem_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            NitnemBani bani = e.SelectedItem as NitnemBani;
            Navigation.PushAsync(new ShabadDetails(bani));
            lstNitnem.SelectedItem = null;
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new EditBaniList());
        }
    }
}