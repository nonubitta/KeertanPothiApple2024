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
    public partial class RaagShabadList : ContentPage
    {
        SQLiteAsyncConnection _con;
		ObservableCollection<VerseSearch> verseOrb = new ObservableCollection<VerseSearch>();

		public Raag _raag { get; set; }
		public RaagShabadList(Raag raag)
		{
			BindingContext = new Theme();
			Title = raag.RaagEnglish;
			_con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
			_raag = raag;
			InitializeComponent();
			LoadRaagShabads();
		}
		protected override void OnAppearing()
		{
			Theme theme = new Theme();
			verseOrb?.ToList().ForEach(a => a.PageBgTheme = theme);
			BindingContext = theme;
			base.OnAppearing();
		}

		private async void LoadRaagShabads()
		{
			using (UserDialogs.Instance.Loading("Loading Shabad List..."))
			{
				List<VerseSearch> verseSearch = await _con.QueryAsync<VerseSearch>(Queries.ShabadByRaagId(_raag.RaagID.Value));
				verseOrb = new ObservableCollection<VerseSearch>(verseSearch);
				lstVerse.ItemsSource = verseOrb;
			}
		}

		private async void lstVerse_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;
			VerseSearch verse = e.SelectedItem as VerseSearch;
			Shabad shabad = await _con.Table<Shabad>().FirstOrDefaultAsync(a => a.VerseID == verse.VerseID);
			await Navigation.PushAsync(new ShabadDetails(shabad.ShabadID, verse.VerseID));
			lstVerse.SelectedItem = null;
		}
	}
}