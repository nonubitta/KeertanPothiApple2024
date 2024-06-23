using DBTest.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Extensions;
using Acr.UserDialogs;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PothiShabadList : ContentPage
    {
        SQLiteAsyncConnection _con;
        public Pothi _pothi { get; set; }
        ObservableCollection<VerseSearch> VerseObs;
        StaticText.PothiShabadsText shabadsText = new StaticText.PothiShabadsText();
        bool isEditable = false;
        int? sortOrder = null;
        public PothiShabadList(Pothi pothi)
        {
            BindingContext = new Theme();
            InitializeComponent();
            Title = pothi.Name;
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            _pothi = pothi;
            EditToolbar.TranslateTo(0, 50, 10, Easing.SinOut);
            EditToolbar.IsVisible = false;
            LoadPothiShabads();
        }

        private async void LoadPothiShabads()
        {
            using (UserDialogs.Instance.Loading("Loading Shabad List..."))
            {
                var test = _con.Table<PothiShabad>();
                List<VerseSearch> verseSearch = await _con.QueryAsync<VerseSearch>(Queries.ShabadByPothiId(_pothi.PothiId.Value));
                VerseObs = new ObservableCollection<VerseSearch>(verseSearch);
                lstVerse.ItemsSource = VerseObs;
                if (VerseObs.Count > 1 || (VerseObs.Count == 1 && !string.IsNullOrWhiteSpace(VerseObs[0].Gurmukhi)))
                {
                    lstVerse.IsVisible = true;
                    slNoShabad.IsVisible = false;
                    btnMove.IsEnabled = true;
                }
                else
                {
                    lstVerse.IsVisible = false;
                    slNoShabad.IsVisible = true;
                    btnMove.IsEnabled = false;
                }
                lblNoShabadAdded1.Text = shabadsText.NoShabadsAdded1;
                lblNoShabadAdded2.Text = shabadsText.NoShabadsAdded2;
                lblNoShabadAdded3.Text = shabadsText.NoShabadsAdded3;
                btnSearch.Text = shabadsText.ButtonText;
            }
        }

        protected override void OnAppearing()
        {
            Theme theme = new Theme();
            VerseObs?.ToList().ForEach(a => a.PageBgTheme = theme);
            BindingContext = theme;
            base.OnAppearing();
        }

        private async void lstVerse_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            VerseSearch verse = e.SelectedItem as VerseSearch;
            Shabad shabad = await _con.Table<Shabad>().FirstOrDefaultAsync(a => a.VerseID == verse.VerseID);
            await Navigation.PushAsync(new ShabadDetails(shabad.ShabadID, verse.VerseID, _pothi.PothiId.Value), true);
            lstVerse.SelectedItem = null;
        }

        private async void share_Clicked(object sender, EventArgs e)
        {
            string json = await Util.GetPothiJson(_pothi);
            Util.ShareFile(json, _pothi.Name + ".json", "Share Pothi");
        }

        private void Edit_Clicked(object sender, EventArgs e)
        {
            isEditable = !isEditable;
            VerseObs.ToList().ForEach(a => a.IsEditable = isEditable);
            btnMove.Text = (isEditable) ? "Done" : "Edit";
            Util.ToggleToolbar(EditToolbar, isEditable, 200);
            using (UserDialogs.Instance.Loading("Updating Shabad..."))
            {
                Task.Delay(10);
                lstVerse.ItemsSource = null;
                lstVerse.ItemsSource = VerseObs;
            }
            //EditToolbar.IsVisible = isEditable;
            //if (isEditable)
            //{
            //    EditToolbar.TranslateTo(0, 0, 200, Easing.SinIn);
            //}
            //else
            //    EditToolbar.TranslateTo(0, 50, 200, Easing.SinOut);
        }

        private List<PothiShabad> GetShabadList()
        {
            List<PothiShabad> selected = new List<PothiShabad>();
            List<VerseSearch> sel = VerseObs.Where(a => a.IsChecked == true).ToList();
            if (sel.Count > 0)
            {
                foreach (VerseSearch vs in sel)
                    selected.Add(GetPothiShabadFromVerseSearch(vs));
            }
            return selected;
        }

        private PothiShabad GetPothiShabadFromVerseSearch(VerseSearch vs)
        {
            PothiShabad pothiShabad = new PothiShabad();
            pothiShabad.ShabadId = vs.ShabadId.Value;
            pothiShabad.VerseId = vs.VerseID;
            pothiShabad.PothiId = _pothi.PothiId.Value;
            pothiShabad.SortOrder = vs.SortOrder;
            pothiShabad.Notes = vs.Notes;
            return pothiShabad;
        }

        private async void Move_Clicked(object sender, EventArgs e)
        {
            //List<PothiShabad> selected = GetShabadList();
            //await Navigation.PushPopupAsync(new SharePopup(selected, SharePopup.ActionType.moved), true);
            //await Delete();
        }

        private async void Copy_Clicked(object sender, EventArgs e)
        {
            List<PothiShabad> selected = GetShabadList();
            await Navigation.PushPopupAsync(new SharePopup(selected, SharePopup.ActionType.copied));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Delete();
        }

        private async Task Delete()
        {
            List<PothiShabad> selected = GetShabadList();
            bool confirm = await DisplayAlert("Delete Shabad(s)?", $"Are you sure you want to delete {selected.Count} shabad(s) from pothi?", "Yes", "No");
            if (confirm)
            {
                bool deleted = await Queries.DeleteMovedShabads(selected);
                if (deleted)
                {
                    List<VerseSearch> sel = VerseObs.Where(a => a.IsChecked == true).ToList();
                    foreach (VerseSearch s in sel)
                    {
                        VerseObs.Remove(s);
                    }
                    Util.ShowRoast("Shabad(s) deleted successfully", true);
                }
            }
        }

        private void MoveUp_Clicked(object sender, EventArgs e)
        {
            VerseSearch search = (sender as ImageButton).CommandParameter as VerseSearch;
            int index = VerseObs.IndexOf(search);
            if (index > 0)
            {
                VerseSearch prior = VerseObs[index - 1];
                VerseObs.Remove(prior);
                VerseObs.Insert(index, prior);

                VerseObs[index].SortOrder = index + 1;
                VerseObs[index - 1].SortOrder = index;
                UpdateShabadMove(VerseObs[index], VerseObs[index - 1]);
            }
        }

        private async void UpdateShabadMove(VerseSearch shabadUp, VerseSearch shabadDown)
        {
            try
            {
                string query = Queries.UpdateSortOrder(_pothi.PothiId.Value, shabadUp.ShabadId.Value, shabadUp.SortOrder.Value);
                int updated = await _con.ExecuteAsync(query);
                if (updated > 0)
                {
                    query = Queries.UpdateSortOrder(_pothi.PothiId.Value, shabadDown.ShabadId.Value, shabadDown.SortOrder.Value);
                    updated = await _con.ExecuteAsync(query);
                }
            }
            catch { }
        }

        private void MoveDown_Clicked(object sender, EventArgs e)
        {
            VerseSearch search = (sender as ImageButton).CommandParameter as VerseSearch;
            int index = VerseObs.IndexOf(search);
            if (index < VerseObs.Count - 1)
            {
                VerseSearch next = VerseObs[index + 1];
                VerseObs.Remove(next);
                VerseObs.Insert(index, next);

                VerseObs[index].SortOrder = index + 1;
                VerseObs[index + 1].SortOrder = index + 2;
                UpdateShabadMove(VerseObs[index], VerseObs[index + 1]);
            }
        }

        private void Search_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Search(), true);
        }
    }
}