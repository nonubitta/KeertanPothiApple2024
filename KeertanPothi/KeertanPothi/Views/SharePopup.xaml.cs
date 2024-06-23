using DBTest.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SharePopup : PopupPage
    {
        private string SelectText = "Select Pothi to add shabad";
        private string NoPothiText = "No existing pothi found";
        public enum RequestSource
        {
            Single,
            Multiple
        }
        public enum ActionType
        {
            added,
            moved,
            copied
        }
        RequestSource RequestType;
        ActionType Action;
        SQLiteAsyncConnection _con;
        private int ShabadId = 0;
        private int? VerseId = null;
        private int? SortOrder = null;
        List<PothiShabad> PothiShabad;
        ObservableCollection<Pothi> PothiOrb;
        public SharePopup(int shabadId, int verseId, ActionType action = ActionType.added)
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            ShabadId = shabadId;
            VerseId = verseId;
            InitializeComponent();
            RequestType = RequestSource.Single;
            Action = action;
            PothiShabad = new List<PothiShabad>();
            LoadLists();
        }
        public SharePopup(List<PothiShabad> pothiShabad, ActionType action)
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            InitializeComponent();
            PothiShabad = pothiShabad;
            RequestType = RequestSource.Multiple;
            Action = action;
            LoadLists();
        }

        protected override void OnAppearing()
        {
            BindingContext = new Theme();
            base.OnAppearing();
        }

        private async void LoadLists()
        {
            List<Pothi> pothis = await _con.QueryAsync<Pothi>(Queries.GetAllPothisWithSortOrder());
            PothiOrb = new ObservableCollection<Pothi>(pothis);
            ddlPothi.ItemsSource = PothiOrb;

            if (pothis.Count == 1)
                ddlPothi.SelectedIndex = 0;
            if (pothis.Count == 0)
            {
                swtExpandSwitch2.IsToggled = true;
                lblSelect.Text = NoPothiText;
                Save.IsVisible = false;
            }
            else
            {
                swtExpandSwitch.IsToggled = true;
                lblSelect.Text = SelectText;
                Save.IsVisible = true;
            }

            List<Verse> verses = await _con.QueryAsync<Verse>(Queries.ShabadById(ShabadId));
            if (verses.Count > 0)
            {
                pckLine.ItemsSource = verses;
                Verse selectedVerse = verses.FirstOrDefault(a => a.ID == VerseId);
                pckLine.SelectedItem = selectedVerse;
                pckLine.IsVisible = true;
            }
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            Pothi pothi = ddlPothi.SelectedItem as Pothi;
            SortOrder = await Queries.GetMaxSortId(pothi.PothiId.Value) ?? 0;
            int? pothiId = pothi.PothiId;
            if (pothiId.HasValue)
            {
                if (RequestType == RequestSource.Single)
                {
                    Verse verse = pckLine.SelectedItem as Verse;
                    PothiShabad pothiShabad = new PothiShabad();
                    pothiShabad.PothiId = pothiId.Value;
                    pothiShabad.ShabadId = ShabadId;
                    pothiShabad.VerseId = verse.ID;
                    pothiShabad.SortOrder = SortOrder + 1;
                    PothiShabad.Add(pothiShabad);
                }
                else
                {
                    PothiShabad.ToList().ForEach(a => a.PothiId = pothiId.Value);
                    int sortOrder = SortOrder.Value + 1;
                    foreach (PothiShabad pothiShabads in PothiShabad)
                    {
                        pothiShabads.PothiId = pothiId.Value;
                        pothiShabads.SortOrder = sortOrder;
                        sortOrder += 1;
                    }
                }
                
                SavePothiShabad(PothiShabad);
                await Navigation.PopPopupAsync();
            }
        }

        private async void SavePothiShabad(List<PothiShabad> pothiShabads)
        {
            foreach(PothiShabad pothiShabad in pothiShabads)
            {
                try
                {
                    await _con.InsertAsync(pothiShabad);
                    Util.ShowRoast($"Shabad {Action.ToString()} to pothi.", true);
                    await Queries.ExportPothis(null, true);
                }
                catch (SQLiteException ex)
                {
                    if (ex.Message.Contains("Constraint"))
                    {
                        Util.ShowRoast("Shabad already exists in Pothi", true);
                    }
                }
                catch (Exception ex)
                {
                    Util.ShowRoast("Failed to add shabad to pothi", true);
                }
            }
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private async void NewPothi_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(entPothiName.Text))
            {
                Pothi pothi = new Pothi();
                pothi.Name = entPothiName.Text;
                pothi.CreatedOn = DateTime.Now;
                var cnt = await _con.InsertAsync(pothi);
                if (cnt > 0)
                {
                    int lastId = await Queries.GetLastId();
                    if (lastId > 0) {
                        pothi.PothiId = lastId;
                        PothiOrb.Add(pothi);
                        ddlPothi.SelectedItem = pothi;
                        if (ddlPothi.Items.Count > 0)
                        {
                            Save_Clicked(null, null);
                        }
                    }
                }

                await Navigation.PopPopupAsync();
            }
        }

        private void btnClose_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private void swtExpandSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            swtExpandSwitch2.IsToggled = !swtExpandSwitch.IsToggled;
        }

        private void swtExpandSwitch2_Toggled(object sender, ToggledEventArgs e)
        {
            swtExpandSwitch.IsToggled = !swtExpandSwitch2.IsToggled;
        }
    }
}