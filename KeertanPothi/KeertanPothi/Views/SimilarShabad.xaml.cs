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
    public partial class SimilarShabad : ContentPage
    {
        //string[] Words = null;
        ObservableCollection<Words> Keywords;
        string VerseSelected = string.Empty;
        SQLiteAsyncConnection _con;
        ObservableCollection<VerseSearch> versesObs;
        Theme theme = new Theme();
        public SimilarShabad(string verse)
        {
            InitializeComponent();
            BindingContext = theme;
            VerseSelected = verse;
            string[] words = verse.Split();
            List<Words> keywords = new List<Words>();
            foreach (string word in words)
            {
                keywords.Add(new Words(word));
            }
            Keywords = new ObservableCollection<Words>(keywords);
            cvVerse.ItemsSource = Keywords;
        }
        protected override void OnAppearing()
        {
        
            versesObs?.ToList().ForEach(a => a.PageBgTheme = theme);
            Keywords?.ToList().ForEach(a => a.PageBgTheme = theme);
            base.OnAppearing();
        }

        private async void Search_Clicked(object sender, EventArgs e)
        {
            using (UserDialogs.Instance.Loading("Searching Shabads..."))
            {
                List<object> items = cvVerse.SelectedItems.ToList();
                List<string> keys = new List<string>();
                //var charsToRemove = new string[] { "i", "I", "e", "E", "u", "U", "y", "Y", "w" };
                string preWord = string.Empty;
                string preCombWord = string.Empty;
                foreach (object obj in items)
                {
                    Words item = obj as Words;
                    string str = item.Item;
                    if (!string.IsNullOrEmpty(preWord) && item.Item == preWord)
                    {
                        string key = item.Item + "%" + preCombWord;
                        keys.Add(key);
                        preWord = item.Item;
                        preCombWord = key;
                    }
                    else
                    {
                        keys.Add(str);
                        preWord = item.Item;
                        preCombWord = item.Item;
                    }
                    //foreach (var c in charsToRemove)
                    //{
                    //    str = str.Replace(c, string.Empty);
                    //}
                }
                _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
                string query = Queries.SimilarShabadSearch(keys);
                List<VerseSearch> verseSearch = await _con.QueryAsync<VerseSearch>(query);
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
            await Navigation.PushAsync(new ShabadDetails(shabad.ShabadID, verse.VerseID));
            lstVerse.SelectedItem = null;
        }

        private void cvVerse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cvVerse.SelectedItems.Count > 0)
                btnSearch.IsEnabled = true;
            else
                btnSearch.IsEnabled = false;
        }
    }
}