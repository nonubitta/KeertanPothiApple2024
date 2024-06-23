using Acr.UserDialogs;
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
    public partial class History : ContentPage
    {
        SQLiteAsyncConnection _con;
        ObservableCollection<VerseSearch> versesObs = new ObservableCollection<VerseSearch>();
        public History()
        {
            BindingContext = new Theme();
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            InitializeComponent();
            LoadHistory();
            Title = "History";
        }
        protected override void OnAppearing()
        {
            Theme theme = new Theme();
            versesObs?.ToList().ForEach(a => a.PageBgTheme = theme);
            BindingContext = theme;
            base.OnAppearing();
        }


        private async void LoadHistory()
        {
            using (UserDialogs.Instance.Loading("Loading History..."))
            {
                List<VerseSearch> verseSearch = await _con.QueryAsync<VerseSearch>(Queries.GetShabadHistory());
                versesObs = new ObservableCollection<VerseSearch>(verseSearch);
                lstVerse.ItemsSource = versesObs;
            }
        }

        private async void lstVerse_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            VerseSearch verse = e.SelectedItem as VerseSearch;
            Shabad shabad = await _con.Table<Shabad>().FirstOrDefaultAsync(a => a.VerseID == verse.VerseID);
            await Navigation.PushAsync(new ShabadDetails(shabad.ShabadID, verse.VerseID, true));
            lstVerse.SelectedItem = null;
        }
    }
}