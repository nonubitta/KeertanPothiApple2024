using DBTest.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NitnemBookmarkPopup : PopupPage
    {
        SQLiteAsyncConnection _con;
        int BaniId;
        ObservableCollection<VerseSearch> BookmarksOrb;
        public delegate void BookmarkSelectedDelegate(int veresId);
        public event BookmarkSelectedDelegate BookmarkSelectedEvent;
        private void InvokeBookmarkSelectedChanged(int veresId)
        {
            BookmarkSelectedEvent?.Invoke(veresId);
        }
        public NitnemBookmarkPopup(int baniId)
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            BindingContext = new Theme();
            BaniId = baniId;
            InitializeComponent();
            LoadBookmarks();
        }
        protected override void OnAppearing()
        {
            Theme theme = new Theme();
            BindingContext = theme;
            base.OnAppearing();
        }

        private async void LoadBookmarks()
        {
            List<VerseSearch> bookmarks = await _con.QueryAsync<VerseSearch>(Queries.BookmarksByNitnemId(BaniId));
            BookmarksOrb = new ObservableCollection<VerseSearch>(bookmarks);
            lstVerse.ItemsSource = BookmarksOrb;
        }

        private void lstVerse_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            VerseSearch verse = e.SelectedItem as VerseSearch;
            InvokeBookmarkSelectedChanged(verse.VerseID);
            Navigation.PopPopupAsync();
        }
        private void btnSave_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}
