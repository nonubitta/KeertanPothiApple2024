using DBTest.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Collections;
using KeertanPothi.model;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShabadDetailsLight : ContentPage
    {
        private NitnemBani _nitnemBani;
        SQLiteAsyncConnection _con;
        ObservableCollection<Verse> versesObs = new ObservableCollection<Verse>();
        public bool ToolbarVisible { get; set; } = true;
        public ShabadDetailsLight(NitnemBani bani)
        {
            this.BindingContext = new Theme();
            this._nitnemBani = bani;
            InitializeComponent();
            LoadShabad();
        }

        private async void LoadShabad()
        {
                _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
                List<Verse> verses;
                string qr = Queries.NitnemBaniShabads(_nitnemBani.BaniId);
                verses = await _con.QueryAsync<Verse>(qr);
                
                versesObs = new ObservableCollection<Verse>(verses);
                if (versesObs.Count > 0)
                {
                    AddVishram();
                    lstShabad.ItemsSource = versesObs;
                }
                else
                    Util.ShowRoast("Error loading Asa ki Vaar...");

        }

        private List<Verse> AddVishram(List<Verse> verses = null)
        {
            if (verses == null)
                verses = versesObs.ToList();
            bool vishraam = Util.PrefShowVishraam;
            if (vishraam)
            {
                foreach (Verse verse in verses)
                {
                    if (verse.Visraam != null && vishraam)
                    {
                        JObject vishram = JObject.Parse(verse.Visraam);
                        string vishraamSource = GetVishraamSource(vishram);
                        if (!string.IsNullOrWhiteSpace(vishraamSource))
                        {
                            string[] line = verse.GurmukhiHtml.Split(' ');
                            int children = vishram[vishraamSource].Children().Count();
                            for (int i = 0; i < children; i++)
                            {
                                string vishraamWord = (string)vishram[vishraamSource][i]["p"];
                                string vishraamType = (string)vishram[vishraamSource][i]["t"];
                                int wordIndex = 0;
                                if (int.TryParse(vishraamWord, out wordIndex))
                                {
                                    string vishraamColor = vishraamType == "v" ? Util.MainVishraamColor : Util.SecondVishraamColor;
                                    line[wordIndex] = $"<font color=\"{vishraamColor}\">{line[wordIndex]}</font>";
                                    verse.GurmukhiHtml = string.Join(" ", line);
                                }
                            }
                        }
                    }

                }
            }
            return verses;
        }

        private string GetVishraamSource(JObject vishram)
        {
            string vishraamSource = string.Empty;
            if (vishram[Util.VishraamSource].HasValues || vishram[Util.VishraamSource].Children().Count() > 0)
            {
                vishraamSource = Util.VishraamSource;
            }
            else if (vishram[Util.VishraamSource2].HasValues || vishram[Util.VishraamSource2].Children().Count() > 0)
            {
                vishraamSource = Util.VishraamSource2;
            }
            else if (vishram[Util.VishraamSource3].HasValues || vishram[Util.VishraamSource3].Children().Count() > 0)
            {
                vishraamSource = Util.VishraamSource3;
            }
            return vishraamSource;
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void menu_Clicked(object sender, EventArgs e)
        {
            if (versesObs.Count > 0)
            {
                SettingsPopup settingsPopup = new SettingsPopup(versesObs[0]);
                //settingsPopup.SettingChangedEvent += SettingChanged;
                //Navigation.PushPopupAsync(settingsPopup);
            }
        }

        private async void FullScreen(bool isFullscreen)
        {
            ToolbarVisible = !ToolbarVisible;

            if (isFullscreen)
            {
                NavigationPage.SetHasNavigationBar(this, false);
                await EditToolbar.TranslateTo(0, 50, 200, Easing.SinOut);
                await Task.Delay(50);
                EditToolbar.IsVisible = false;
            }
            else
            {
                NavigationPage.SetHasNavigationBar(this, true);
                EditToolbar.IsVisible = true;
                await EditToolbar.TranslateTo(0, 0, 200, Easing.SinOut);
            }
        }

        private void btnMaximize_Clicked(object sender, EventArgs e)
        {
            FullScreen(true);
        }

        private void btnRestore_Clicked(object sender, EventArgs e)
        {
            FullScreen(false);
        }

        private void bookmark_Clicked(object sender, EventArgs e)
        {
            NitnemBookmarkPopup bookmarkPopup = new NitnemBookmarkPopup(_nitnemBani.BaniId);
            bookmarkPopup.BookmarkSelectedEvent += BookmarkSelected;
            Navigation.PushPopupAsync(bookmarkPopup);
        }

        private void BookmarkSelected(int verseId)
        {
            if (verseId > 0)
            {
                Verse SelectedVerse = versesObs.FirstOrDefault(a => a.ID == verseId);
                lstShabad.ScrollTo(SelectedVerse, ScrollToPosition.MakeVisible, false);
            }
        }
    }
}