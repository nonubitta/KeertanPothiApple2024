using DBTest.Models;
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
    public partial class RaagList : ContentPage
    {
        SQLiteAsyncConnection _con;
        ObservableCollection<Raag> raagsOrb = new ObservableCollection<Raag>();
        public RaagList()
        {
            BindingContext = new Theme();
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            InitializeComponent();
            LoadRaags();
        }
        protected override void OnAppearing()
        {
            Theme theme = new Theme();
            raagsOrb?.ToList().ForEach(a => a.PageBgTheme = theme);
            BindingContext = theme;
            base.OnAppearing();
        }
        private async void LoadRaags()
        {
            List<Raag> raags = await _con.QueryAsync<Raag>(Queries.GetAllRaags());
            raagsOrb = new ObservableCollection<Raag>(raags);
            lstRaag.ItemsSource = raagsOrb;
        }

        private async void lstRaag_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            Raag raag = e.SelectedItem as Raag;
            await Navigation.PushAsync(new RaagShabadList(raag));
            lstRaag.SelectedItem = null;
        }
    }
}