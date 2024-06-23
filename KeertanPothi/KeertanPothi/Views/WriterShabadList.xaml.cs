using DBTest.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WriterShabadList : ContentPage
	{
        SQLiteAsyncConnection _con;
		public Writer _writer { get; set; }
		List<WriterInfo> winfo = null;
		public WriterShabadList(Writer writer)
        {
            InitializeComponent();
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
			_writer = writer;
			Title = writer.WriterEnglish;
			LoadWriterInfo();
			LoadWriterData();
		}

		private async void LoadWriterInfo()
		{
			try
			{
				winfo = await _con.QueryAsync<WriterInfo>(Queries.GetWriteInfoByid(_writer.WriterID));
				if (winfo == null || winfo.Count == 0 || !winfo[0].IsGuru)
				{
					ToolbarItems.RemoveAt(0);
				}
			}
			catch(Exception ex)
			{

			}
			
		}

		protected override void OnAppearing()
		{
			Theme theme = new Theme();
			BindingContext = theme;
			base.OnAppearing();
		}

		private async void LoadWriterData()
		{
			using (UserDialogs.Instance.Loading("Loading Shabad List..."))
			{
				List<VerseSearch> verseSearch = await _con.QueryAsync<VerseSearch>(Queries.ShabadByWriterId(_writer.WriterID));
				lstVerse.ItemsSource = verseSearch;

		
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

		private void WriterInfo_Clicked(object sender, EventArgs e)
		{
			Navigation.PushPopupAsync(new ShowWriterInfo(winfo));
		}
	}
}