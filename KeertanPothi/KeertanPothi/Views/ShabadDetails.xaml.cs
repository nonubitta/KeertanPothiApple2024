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
using Rg.Plugins.Popup.Extensions;
using System.Collections.ObjectModel;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using KeertanPothi.model;
using Acr.UserDialogs;
using Plugin.SimpleAudioPlayer;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShabadDetails : ContentPage
    {
        #region Properties

        private bool initializing = false;
        SQLiteAsyncConnection _con;
        public int ShabadId { get; set; }
        public int? SelectedVerseId { get; set; }
        public Verse SelectedVerse { get; set; }
        public bool ToolbarVisible { get; set; } = true;
        ObservableCollection<Verse> versesObs = new ObservableCollection<Verse>();
        public int PothiId { get; set; }
        public int AngNo { get; set; }
        List<PothiShabad> pothiShabadList;
        private NitnemBani _nitnemBani;
        double lastYPos = 0;
        ISimpleAudioPlayer AudioPlayer;
        ToolbarItem itemPlay = null;
        ToolbarItem itemBookmark = null;
        ToolbarItem itemKeertanMode = null;
        ToolbarItem itemShare = null;
        ToolbarItem itemShare2 = null;
        ToolbarItem itemShare3 = null;
        ToolbarItem itemNote = null;
        ToolbarItem itemSimilar = null;
        private Verse similarShabadVerse = null;
        private Label lblScripture2;
        public enum RequestSource
        {
            Search,
            Random,
            Pothi,
            Set,
            Ang,
            Nitnem,
            History
        }

        public RequestSource RequestFrom { get; set; }

        #endregion

        #region Constructors 

        /// <summary>
        /// User coming from Shabad search.
        /// </summary>
        /// <param name="shabadID"></param>
        /// <param name="verseID"></param>
        public ShabadDetails(int shabadID, int? verseID = null, bool fromHistory = false)
        {
            this.BindingContext = new Theme();
            initializing = true;
            ShabadId = shabadID;
            SelectedVerseId = verseID;
            if (fromHistory)
                RequestFrom = RequestSource.History;
            else
                RequestFrom = RequestSource.Search;
            CommonConstructor();
            initializing = false;
        }

        /// <summary>
        /// User coming from Random shabad
        /// </summary>
        public ShabadDetails()
        {
            this.BindingContext = new Theme();
            initializing = true;
            var rand = new Random();
            ShabadId = rand.Next(20, 5540);
            RequestFrom = RequestSource.Random;
            CommonConstructor();
            initializing = false;
        }

        /// <summary>
        /// User coming from Pothi shabad list
        /// </summary>
        /// <param name="shabadID"></param>
        /// <param name="verseID"></param>
        /// <param name="pothiId"></param>
        public ShabadDetails(int shabadID, int verseID, int pothiId)
        {
            this.BindingContext = new Theme();
            initializing = true;
            ShabadId = shabadID;
            SelectedVerseId = verseID;
            PothiId = pothiId;
            RequestFrom = RequestSource.Pothi;
            CommonConstructor();
            initializing = false;
        }

        /// <summary>
        /// User coming from Ang Search
        /// </summary>
        /// <param name="angNo"></param>
        public ShabadDetails(int angNo)
        {
            this.BindingContext = new Theme();
            initializing = true;
            AngNo = angNo;
            RequestFrom = RequestSource.Ang;
            CommonConstructor();
            initializing = false;
        }

        /// <summary>
        /// User coming from Nitnetm
        /// </summary>
        /// <param name="bani"></param>
        public ShabadDetails(NitnemBani bani)
        {
            this.BindingContext = new Theme();
            initializing = true;
            this._nitnemBani = bani;
            RequestFrom = RequestSource.Nitnem;
            CommonConstructor();
            initializing = false;
        }

        private void CommonConstructor()
        {
            CreateToolbars();
            InitializeComponent();
            LoadShabad();
        }

        #endregion

        #region Toolbars

        private void CreateToolbars()
        {
            ToolbarItems.Clear();
            CreateSettingToolbar();
            if (RequestFrom == RequestSource.Nitnem)
            {
                if (_nitnemBani.Bookmark)
                {
                    CreateBookmarkToolbar();
                }
                AudioPlayer = CrossSimpleAudioPlayer.Current;
                try
                {
                    AudioPlayer.Load(_nitnemBani.BaniId + ".mp3");
                    CreatePlayToolbar();
                }
                catch { }
                if (_nitnemBani.Id == 18) //Asa ki vaar
                {
                    CreateKeertanMode();
                }
            }
            else
            {
                //CreateShareToolbar();
                //CreateSimilarShabadToolbar();
                CreateSharePopupToolbar();
            }
        }

        private void CreateNoteToolbar()
        {
            itemKeertanMode = new ToolbarItem();
            itemKeertanMode.Text = "Text Note";
            itemKeertanMode.Order = ToolbarItemOrder.Secondary;
            //itemKeertanMode.Clicked += Note_Clicked;
            ToolbarItems.Add(itemKeertanMode);
        }

        private void CreateSettingToolbar()
        {
            itemKeertanMode = new ToolbarItem();
            itemKeertanMode.Text = "Settings";
            itemKeertanMode.IconImageSource = ImageSource.FromResource("KeertanPothi.images.settings2.png");
            itemKeertanMode.Order = ToolbarItemOrder.Primary;
            itemKeertanMode.Clicked += menu_Clicked;
            ToolbarItems.Add(itemKeertanMode);
        }

        private void CreateSimilarShabadToolbar()
        {
            itemSimilar = new ToolbarItem();
            itemSimilar.Text = "";
            itemSimilar.Order = ToolbarItemOrder.Primary;
            itemSimilar.Priority = 0;
            itemSimilar.Clicked += SimilarShabad_Clicked;
            itemSimilar.IsEnabled = false;
            ToolbarItems.Add(itemSimilar);
        }

        private void CreateKeertanMode()
        {
            itemKeertanMode = new ToolbarItem();
            itemKeertanMode.Text = "Keertan";
            itemKeertanMode.IconImageSource = ImageSource.FromResource("KeertanPothi.images.harmonium.png");
            itemKeertanMode.Order = ToolbarItemOrder.Primary;
            itemKeertanMode.Clicked += KeertanMode_Clicked;
            ToolbarItems.Add(itemKeertanMode);
        }

        private void CreateBookmarkToolbar()
        {
            itemBookmark = new ToolbarItem();
            itemBookmark.Text = "Bookmark";
            itemBookmark.IconImageSource = ImageSource.FromResource("KeertanPothi.images.bookmark.png");
            itemBookmark.Order = ToolbarItemOrder.Primary;
            itemBookmark.Clicked += Bookmark_Clicked;
            ToolbarItems.Add(itemBookmark);
        }

        private void CreatePlayToolbar()
        {
            itemPlay = new ToolbarItem();
            itemPlay.Text = "Play";
            itemPlay.IconImageSource = ImageSource.FromResource("KeertanPothi.images.Play.png");
            itemPlay.Order = ToolbarItemOrder.Primary;
            itemPlay.Clicked += Play_Clicked;
            ToolbarItems.Add(itemPlay);
        }

        private void CreateSharePopupToolbar()
        {
            itemShare = new ToolbarItem();
            //itemShare.Text = "Share";
            itemShare.IconImageSource = ImageSource.FromResource("KeertanPothi.images.actionMenu70.png");
            itemShare.Order = ToolbarItemOrder.Primary;
            itemShare.Clicked += SharePopup_Clicked;
            ToolbarItems.Add(itemShare);
        }

        private void CreateShareToolbar()
        {
            itemShare = new ToolbarItem();
            itemShare.Text = "Share as Text";
            itemShare.Order = ToolbarItemOrder.Secondary;
            itemShare.Clicked += share_Clicked;
            ToolbarItems.Add(itemShare);

            itemShare2 = new ToolbarItem();
            itemShare2.Text = "Share as File";
            itemShare2.Order = ToolbarItemOrder.Secondary;
            itemShare2.Clicked += shareFile_Clicked;
            ToolbarItems.Add(itemShare2);

            itemShare3 = new ToolbarItem();
            itemShare3.Text = "Copy to clipboard";
            itemShare3.Order = ToolbarItemOrder.Secondary;
            itemShare3.Clicked += Clipboard_Clicked;
            ToolbarItems.Add(itemShare3);
        }

        #endregion

        #region General

        protected override void OnAppearing()
        {
            if (RequestFrom != RequestSource.Ang && RequestFrom != RequestSource.Nitnem && RequestFrom != RequestSource.History)
                Queries.SaveShabadToHistory(ShabadId, SelectedVerseId);

            Theme theme = new Theme();
            versesObs?.ToList().ForEach(a => a.PageBgTheme = theme);
            BindingContext = theme;
            EditToolbar.BackgroundColor = Color.FromHex(theme.HeaderColor);
            if (lblScripture2 != null && string.IsNullOrEmpty(Title))
                Title = lblScripture2.Text;

            Util.SaveLastShabad(ShabadId, SelectedVerseId, PothiId, AngNo, RequestFrom.ToString());
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            AudioPlayer?.Stop();
            Util.RemoveLastShabad();
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<App, object>(this, "NoteUpdated");
        }

        private async void LoadShabad()
        {
            //MessagingCenter.Subscribe<App, string>((App)Application.Current, "NoteUpdated", (sender, arg) => {
            //    OnNoteUpdated(arg);
            //});
      
            using (UserDialogs.Instance.Loading("Loading Shabad..."))
            {
                if(_con == null)
                    _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
                List<Verse> verses;
                string notes = string.Empty;
                //if (RequestFrom == RequestSource.Pothi) {
                //    notes = await _con.ExecuteScalarAsync<string>(Queries.GetNotes(PothiId, ShabadId));
                //    if (!string.IsNullOrEmpty(notes))
                //        ToggleNotes(notes);
                //}
                if (RequestFrom == RequestSource.Ang)
                {
                    verses = await _con.QueryAsync<Verse>(Queries.VerseByAng(AngNo));
                }
                else if (RequestFrom == RequestSource.Nitnem)
                {
                    string qr = Queries.NitnemBaniShabads(_nitnemBani.BaniId);
                    verses = await _con.QueryAsync<Verse>(qr);
                }
                else
                {
                    string qr = Queries.ShabadById(ShabadId);
                    verses = await _con.QueryAsync<Verse>(qr);
                }
                if (verses.Count > 0)
                {
                    versesObs = new ObservableCollection<Verse>(verses);
                    Theme theme = new Theme();
                    AddVishram();
                    ShowLadivaar();
                    versesObs.ToList().ForEach(a=> a.GurmukhiHtml = $"<font color=\"{theme.FontColor}\"> {a.GurmukhiHtml}</font>");

                    lstShabad.ItemsSource = versesObs;
                    Verse verse = verses.FirstOrDefault(a => a.WriterID != null);
                   
                    if (SelectedVerseId != null && SelectedVerseId > 0)
                    {
                        lstShabad.ListViewCachingStrategy = Syncfusion.ListView.XForms.ListViewCachingStrategy.CreateNewTemplate;
                        SelectedVerse = versesObs.FirstOrDefault(a => a.ID == SelectedVerseId);
                        lstShabad.ScrollTo(SelectedVerse, Syncfusion.ListView.XForms.ScrollToPosition.Start, false);
                        SelectedVerse.ListBgColor = SelectedVerse?.PageBgTheme.DefaultItemBg;
                    }
                    else
                        lstShabad.ScrollTo(versesObs[0], Syncfusion.ListView.XForms.ScrollToPosition.Start, false);

                    if (verse != null)
                        LoadShabadDetails(verses[0], verse?.WriterEnglish);
                }
                else
                    Util.ShowRoast("Error loading shabad...");
            }
        }

        //private void ToggleNotes(string notes)
        //{
        //    if (!string.IsNullOrEmpty(notes))
        //    {
        //        editNotes.Text = notes;
        //        expNotes.IsVisible = true;
        //    }
        //    else
        //    {
        //        expNotes.IsVisible = false;
        //        editNotes.Text = string.Empty;
        //    }
        //}

        //private void OnNoteUpdated(string note)
        //{
        //    if (!string.IsNullOrWhiteSpace(note))
        //    {
        //        editNotes.Text = note;
        //        expNotes.IsExpanded = true;
        //        Util.ShowRoast("Note Saved.", true);
        //    }
        //    else
        //    {
        //        expNotes.IsVisible = false;
        //        Util.ShowRoast("Note Deleted.", true);
        //    }
        //    LoadShabad();
        //    UserDialogs.Instance.HideLoading();
        //}

        private void LoadShabadDetails(Verse verse, string writerEnglish)
        {
            lstShabad.HeaderTemplate = new DataTemplate(() =>
            {
                var stack = new StackLayout();
                stack.Spacing = 0;
                stack.Padding = new Thickness(20, 10, 20, 10);
                stack.BackgroundColor = Color.FromHex("#5e6573");
                var headerLabel = new Label
                {
                    TextColor = Color.FromHex("#cfe2f3"),
                    FontSize = 12,
                    FontAttributes = FontAttributes.Bold,
                    Text = "Scripture: " + verse.SourceEnglish
                };
                var headerLabel2 = new Label
                {
                    TextColor = Color.FromHex("#cfe2f3"),
                    FontSize = 12,
                    FontAttributes = FontAttributes.Bold,
                    Text = "Ang: " + verse.PageNo.ToString()
                };
                var headerLabel4 = new Label
                {
                    TextColor = Color.FromHex("#cfe2f3"),
                    FontSize = 12,
                    FontAttributes = FontAttributes.Bold,
                    Text = "Writer: " + writerEnglish
                };
                var headerLabel3 = new Label
                {
                    TextColor = Color.FromHex("#cfe2f3"),
                    FontSize = 12,
                    FontAttributes = FontAttributes.Bold,
                    Text = "Raag: " + verse.RaagEnglish
                };
                stack.Children.Add(headerLabel);
                stack.Children.Add(headerLabel2);
                stack.Children.Add(headerLabel4);
                stack.Children.Add(headerLabel3);

                return stack;
            });
            //lblScripture.Text = "Scripture: " + verse.SourceEnglish;
            //lblAng.Text = "Ang: " + verse.PageNo.ToString();
            //lblWriter.Text = writerEnglish;
            //lblRaag.Text = "Raag: " + verse.RaagEnglish;
            if (RequestFrom == RequestSource.Ang)
                Title = "Ang: " + verse.PageNo.ToString();
            else if (RequestFrom == RequestSource.Nitnem)
                Title = _nitnemBani.EnglishName;
            //else if (RequestFrom == RequestSource.Pothi)
            //    Title = Pothi
            else
                Title = writerEnglish;
        }

        private async void LoadMoreShabads()
        {
            string[] shabadList = _nitnemBani.ShabadList.Split(',');
            Verse breaker = new Verse();
            breaker.Gurmukhi = "***************";
            breaker.GurmukhiHtml = "<div style='text-align:center; '>***************</div>";
            for (int i = 1; i < shabadList.Count(); i++)
            {
                int shabadId = 0;
                if (int.TryParse(shabadList[i], out shabadId))
                {
                    List<Verse> verses = await _con.QueryAsync<Verse>(Queries.ShabadById(shabadId));
                    if (Util.PrefShowVishraam)
                        verses = AddVishram(verses);
                    if (!_nitnemBani.IsSingleBani)
                        verses.Insert(0, breaker);
                    foreach (Verse verse in verses)
                        versesObs.Add(verse);
                }
            }
        }

        private void ReloadGrid()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                using (UserDialogs.Instance.Loading("Updating Shabad..."))
                {
                    Task.Delay(100);
                    lstShabad.ItemsSource = null;
                    lstShabad.ItemsSource = versesObs;
                }
            }
            
        }

        private void SettingChanged(Util.SettingName settingName, bool? value)
        {
            switch (settingName)
            {
                case Util.SettingName.GurmukhiVisible:
                    versesObs.ToList().ForEach(a => a.GurmukhiVisible = value.Value);
                    //ReloadGrid();
                    break;

                case Util.SettingName.EngTranslationVisible:
                    versesObs.ToList().ForEach(a => a.TranslationVisible = value.Value);
                    //ReloadGrid();
                    break;

                case Util.SettingName.PunTranslationVisible:
                    versesObs.ToList().ForEach(a => a.PunjabiTranslationVisible = value.Value);
                    //ReloadGrid();
                    break;

                case Util.SettingName.TransliterationVisible:
                    versesObs.ToList().ForEach(a => a.TransliterationVisible = value.Value);
                    //ReloadGrid();
                    break;

                case Util.SettingName.GurmukhiFontSize:
                    if (value.Value)
                        versesObs.ToList().ForEach(a => a.GurmukhiFontSize += 2);
                    else
                    {
                        if (versesObs[0].GurmukhiFontSize >= 8)
                            versesObs.ToList().ForEach(a => a.GurmukhiFontSize -= 2);
                    }
                    //ReloadGrid();
                    break;

                case Util.SettingName.EngTranslationFontSize:
                    if (value.Value)
                        versesObs.ToList().ForEach(a => a.EnglishFontSize += 2);
                    else
                    {
                        if (versesObs[0].EnglishFontSize >= 8)
                            versesObs.ToList().ForEach(a => a.EnglishFontSize -= 2);
                    }
                    //ReloadGrid();
                    break;

                case Util.SettingName.PunTranslationFontSize:
                    if (value.Value)
                        versesObs.ToList().ForEach(a => a.PunjabiFontSize += 2);
                    else
                    {
                        if (versesObs[0].PunjabiFontSize >= 8)
                            versesObs.ToList().ForEach(a => a.PunjabiFontSize -= 2);
                    }
                    //ReloadGrid();
                    break;

                case Util.SettingName.TransliterationFontSize:
                    if (value.Value)
                        versesObs.ToList().ForEach(a => a.TransliterationFontSize += 2);
                    else
                    {
                        if (versesObs[0].TransliterationFontSize >= 8)
                            versesObs.ToList().ForEach(a => a.TransliterationFontSize -= 2);
                    }
                    //ReloadGrid();
                    break;

                case Util.SettingName.FullScreen:
                    //FullScreen(value.Value);
                    break;

                case Util.SettingName.NormalFont:
                    string fontName = value.Value ? Util.PunjabiFontKey : Util.HandFontKey;
                    versesObs.ToList().ForEach(a => a.GurmukhiFontName = fontName);
                    //ReloadGrid();
                    break;

                case Util.SettingName.HandFont:
                    string fontName2 = value.Value ? Util.HandFontKey : Util.PunjabiFontKey;
                    versesObs.ToList().ForEach(a => a.GurmukhiFontName = fontName2);
                    //ReloadGrid();
                    break;

                case Util.SettingName.Ladivaar:
                    VishraamLadivaar();
                    //ReloadGrid();
                    break;

                case Util.SettingName.Vishraam:
                    VishraamLadivaar();
                    //ReloadGrid();
                    break;

                case Util.SettingName.Theme:
                    Theme theme = new Theme();
                    versesObs?.ToList().ForEach(a => a.PageBgTheme = theme);
                    BindingContext = theme;
                    Util.SetStatusBarColor(Color.FromHex(theme.HeaderColor));
                    MasterDetailPage curPage = (MasterDetailPage)Application.Current.MainPage;
                    if(curPage != null)
                    {
                        ((NavigationPage)curPage.Detail).BarBackgroundColor = Color.FromHex(theme.HeaderColor);
                        EditToolbar.BackgroundColor = Color.FromHex(theme.HeaderColor);
                    }

                    if (SelectedVerseId != null && SelectedVerseId > 0)
                    {
                        SelectedVerse = versesObs.FirstOrDefault(a => a.ID == SelectedVerseId);
                        SelectedVerse.ListBgColor = SelectedVerse.PageBgTheme.DefaultItemBg;
                    }
                    VishraamLadivaar();
                    break;
            }
        }

        private async void FullScreen(bool isFullscreen)
        {
            ToolbarVisible = !ToolbarVisible;

            if (isFullscreen)
            {
                NavigationPage.SetHasNavigationBar(this, false);
                slMain.Padding = new Thickness(0, 32, 0, 0);
                await EditToolbar.TranslateTo(0, 50, 200, Easing.SinOut);
                await Task.Delay(50);
                EditToolbar.IsVisible = false;
                btnRestore.IsVisible = true;
                await btnRestore.TranslateTo(0, 0, 500, Easing.SinOut);
            }
            else
            {
                btnRestore.IsVisible = false;
                slMain.Padding = new Thickness(0, 0, 0, 0);
                NavigationPage.SetHasNavigationBar(this, true);
                EditToolbar.IsVisible = true;
                await EditToolbar.TranslateTo(0, 0, 200, Easing.SinOut);
                await btnRestore.TranslateTo(0, 70, 200, Easing.SinOut);
                await Task.Delay(200);
            }
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
            if (vishram[Util.VishraamSource] != null && (vishram[Util.VishraamSource].HasValues || vishram[Util.VishraamSource].Children().Count() > 0))
            {
                vishraamSource = Util.VishraamSource;
            }
            else if (vishram[Util.VishraamSource2] != null && (vishram[Util.VishraamSource2].HasValues || vishram[Util.VishraamSource2].Children().Count() > 0))
            {
                vishraamSource = Util.VishraamSource2;
            }
            else if (vishram[Util.VishraamSource3] != null && (vishram[Util.VishraamSource3].HasValues || vishram[Util.VishraamSource3].Children().Count() > 0))
            {
                vishraamSource = Util.VishraamSource3;
            }
            return vishraamSource;
        }

        private void ShowLadivaar()
        {
            bool ladivaar = Util.PrefShowLadivaar;
            if (ladivaar)
            {
                versesObs.ToList().ForEach(a => a.GurmukhiHtml = a.GurmukhiHtml?.Replace(" ", ""));
                versesObs.ToList().ForEach(a => a.GurmukhiHtml = a.GurmukhiHtml?.Replace("fontcolor", "font color"));
            }
        }

        private void VishraamLadivaar()
        {
            Theme theme = new Theme();
            versesObs.ToList().ForEach(a => a.GurmukhiHtml = a.Gurmukhi);
            AddVishram();
            ShowLadivaar();
            versesObs.ToList().ForEach(a => a.GurmukhiHtml = $"<font color=\"{theme.FontColor}\"> {a.GurmukhiHtml}</font>");
        }

        #endregion

        #region buttons and taps

        private void menu_Clicked(object sender, EventArgs e)
        {
            if (versesObs.Count > 0)
            {
                SettingsPopup settingsPopup = new SettingsPopup(versesObs[0]);
                settingsPopup.SettingChangedEvent += SettingChanged;
                Navigation.PushPopupAsync(settingsPopup);
            }
        }

        private void QuickSettings(object sender, EventArgs e)
        {
            if (Util.PrefPunjabiVisible || Util.PrefEnglishVisible)
            {
                if (Util.PrefPunjabiVisible)
                {
                    SettingChanged(Util.SettingName.PunTranslationVisible, false);
                    Util.PrefPunjabiVisible = false;
                }

                if (Util.PrefEnglishVisible)
                {
                    SettingChanged(Util.SettingName.EngTranslationVisible, false);
                    Util.PrefEnglishVisible = false;
                }
            }
            else
            {
                SettingChanged(Util.SettingName.PunTranslationVisible, true);
                SettingChanged(Util.SettingName.EngTranslationVisible, true);
                Util.PrefPunjabiVisible = true;
                Util.PrefEnglishVisible = true;
            }
        }


        private void share_Clicked(object sender, EventArgs e)
        {
            string str = $"Shared from Keertan Pothi(Id:{ShabadId.ToString()})\r\n";
            foreach (Verse verse in versesObs)
            {
                str += verse.GurmukhiUni + "\r\n";
            }
            Util.ShareText(str);
            //Util.ShareFile(str, "Shabad.txt", "Share Shabad");
        }

        private void shareFile_Clicked(object sender, EventArgs e)
        {
            string str = $"<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/></head><body style=\"background-color:{versesObs[0].PageBgTheme.PageBg}; color:{versesObs[0].PageBgTheme.FontColor}; font-size:40; \">" +
                $"<span style=\"font-size:20\">Shared from Keertan Pothi(Id:{ShabadId.ToString()})<span>\r\n";
            foreach (Verse verse in versesObs)
            {
                str += "<br>" + HtmlFileVishraam(verse) + "\r\n";
            }
            str += "<br></body></html>";
            Util.ShareFile(str, "Shabad.html", "Share Shabad");
        }

        private async void Clipboard_Clicked(object sender, EventArgs e)
        {
            string str = string.Empty;
            foreach (Verse verse in versesObs)
            {
                str += verse.GurmukhiUni + "\r\n";
            }
            await Clipboard.SetTextAsync(str);
        }
        
        private string HtmlFileVishraam(Verse verse)
        {
            bool vishraam = Util.PrefShowVishraam;
            if (vishraam)
            {
                    if (verse.Visraam != null && vishraam)
                    {
                        JObject vishram = JObject.Parse(verse.Visraam);
                        string vishraamSource = GetVishraamSource(vishram);
                        if (!string.IsNullOrWhiteSpace(vishraamSource))
                        {
                            string[] line = verse.GurmukhiUni.Split(' ');
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
                                    verse.GurmukhiUni = string.Join(" ", line);
                                }
                            }
                        }
                    }
            }
            return verse.GurmukhiUni;
        }

        private void addPothi_Clicked(object sender, EventArgs e)
        {
            if (RequestFrom == RequestSource.Nitnem)
            {
                Util.ShowRoast("Can't add Nitnem to keertan pothi", true);
                return;
            }
            int? vId = SelectedVerseId;
            if (!vId.HasValue || vId < 1)
            {
                vId = versesObs.FirstOrDefault(a => a.WriterID != null)?.ID ?? versesObs[0].ID;
            }
            Navigation.PushPopupAsync(new SharePopup(ShabadId, vId.Value));
        }

        void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            FullScreen(ToolbarVisible);
        }

        private void PrevVerse_Clicked(object sender, EventArgs e)
        {
            LoadNextVerse(false);
        }

        private void NextVerse_Clicked(object sender, EventArgs e)
        {
            LoadNextVerse(true);
        }

        /// <summary>
        /// true for nextone, false for prior
        /// </summary>
        /// <param name="nxtOne"></param>
        private async void LoadNextVerse(bool nxtOne)
        {
            int newShabadId = 0;
            switch (RequestFrom)
            {
                case RequestSource.Nitnem:

                    List<NitnemBani> banis = await _con.QueryAsync<NitnemBani>(Queries.GetNextPrevBanis(_nitnemBani.BaniId, nxtOne));
                    if (banis != null && banis.Count > 0 && _nitnemBani != banis[0])
                    {
                        _nitnemBani = banis[0];
                        LoadShabad();
                    }
                    break;
                case RequestSource.Search:
                case RequestSource.Random:
                case RequestSource.History:
                    if (nxtOne)
                        newShabadId = ShabadId + 1;
                    else
                        newShabadId = ShabadId - 1;
                    SelectedVerseId = null;
                    break;
                case RequestSource.Ang:
                    if (nxtOne)
                        AngNo += 1;
                    else
                        AngNo -= 1;
                    break;
                case RequestSource.Pothi:
                    if (pothiShabadList == null)
                        pothiShabadList = await _con.QueryAsync<PothiShabad>($"Select * from PothiShabad where PothiId = {PothiId} order by SortOrder");
                    int curInd = 0;
                    foreach (PothiShabad s in pothiShabadList)
                    {
                        if (s.ShabadId == ShabadId)
                        {
                            curInd = pothiShabadList.IndexOf(s);
                            break;
                        }
                    }

                    if (nxtOne)
                    {
                        if (curInd < pothiShabadList.Count - 1)
                        {
                            newShabadId = pothiShabadList[curInd + 1].ShabadId;
                            SelectedVerseId = pothiShabadList[curInd + 1].VerseId;
                        }
                    }
                    else
                    {
                        if (curInd > 0)
                        {
                            newShabadId = pothiShabadList[curInd - 1].ShabadId;
                            SelectedVerseId = pothiShabadList[curInd - 1].VerseId;
                        }
                    }
                    break;
                case RequestSource.Set:
                    break;
                default:
                    if (nxtOne)
                        newShabadId = ShabadId + 1;
                    else
                        newShabadId = ShabadId - 1;
                    break;
            }

            if ((newShabadId != 0 && newShabadId != ShabadId) || RequestFrom == RequestSource.Ang)
            {
                ShabadId = newShabadId;
                lstShabad.ScrollTo(versesObs[0], Syncfusion.ListView.XForms.ScrollToPosition.Start, false);
                LoadShabad();
            }
            else
            {
                if (RequestFrom == RequestSource.Pothi)
                {
                    if (nxtOne)
                        Util.ShowRoast("This is the last shabad in the pothi", true);
                    else
                        Util.ShowRoast("This is the first shabad in the pothi", true);
                }
            }
        }

        private void btnMaximize_Clicked(object sender, EventArgs e)
        {
            FullScreen(ToolbarVisible);
        }

        private void btnRestore_Clicked(object sender, EventArgs e)
        {
            FullScreen(false);
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        private void Play_Clicked(object sender, EventArgs e)
        {
            if (AudioPlayer.IsPlaying)
            {
                AudioPlayer.Pause();
                itemPlay.IconImageSource = ImageSource.FromResource("KeertanPothi.images.Play.png");
            }
            else
            {
                AudioPlayer.Play();
                itemPlay.IconImageSource = ImageSource.FromResource("KeertanPothi.images.Pause.png");
            }
        }

        private void Bookmark_Clicked(object sender, EventArgs e)
        {
            NitnemBookmarkPopup bookmarkPopup = new NitnemBookmarkPopup(_nitnemBani.BaniId);
            bookmarkPopup.BookmarkSelectedEvent += BookmarkSelected;
            Navigation.PushPopupAsync(bookmarkPopup);
        }

        private async void SharePopup_Clicked(object sender, EventArgs e)
        {
            const string text = "Share as Text";
            const string file = "Share as File";
            const string clipboard = "Copy to clipboard";
            const string notepad = "Text Note";
            //const string similar = "Find similar shabads";
            string action = string.Empty;
            if (RequestFrom == RequestSource.Pothi)
                action = await DisplayActionSheet("Export/Import", "Cancel", null, text, file, clipboard, notepad);
            else
                action = await DisplayActionSheet("Actions", "Cancel", null, text, file, clipboard);

            switch (action)
            {
                case text:
                    share_Clicked(null, null);
                    break;
                case file:
                    shareFile_Clicked(null, null);
                    break;
                case clipboard:
                    Clipboard_Clicked(null, null);
                    break;
                case notepad:
                    //Note_Clicked(null, null);
                    break;
                //case similar:
                //    //if (lstShabad.SelectedItem == null)
                //    Util.ShowRoast("Please select a verse(line) to Find similar shabads", true);
                //    Title = similar;
                //    //lstShabad.SelectionMode = ListViewSelectionMode.Single;
                //    //else
                //    //    FindSimilarShabads();
                //    break;
                default:
                    break;
            }
            //ShareShabadPopup shareShabadPopup = new ShareShabadPopup(versesObs, ShabadId);
            //Navigation.PushPopupAsync(shareShabadPopup);
        }

        private void KeertanMode_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AsaDiVaar());
        }

        //private void Note_Clicked(object sender, EventArgs e)
        //{
        //    string abc = editNotes.Text;
        //    editNotes.Text = string.Empty;
        //    expNotes.IsExpanded = false;

        //    Navigation.PushPopupAsync(new NotesPopup(PothiId, ShabadId, abc));
        //}

        private void BookmarkSelected(int verseId)
        {
            if (verseId > 0)
            {
                SelectedVerse = versesObs.FirstOrDefault(a => a.ID == verseId);
                lstShabad.ScrollTo(SelectedVerse, Syncfusion.ListView.XForms.ScrollToPosition.Start, false);
            }
        }
        
        private void SwipeNext(object sender, EventArgs e)
        {
            LoadNextVerse(true);
        }

        private void SwipePrevious(object sender, EventArgs e)
        {
            LoadNextVerse(false);
        }

        private void SimilarShabad_Clicked(object sender, EventArgs e)
        {
            SimilarShabad similarShabad = new SimilarShabad(similarShabadVerse.Gurmukhi);
            Navigation.PushAsync(similarShabad);
        }

        private void FindSimilarShabads()
        {
            Verse verse = lstShabad.SelectedItem as Verse;
            SimilarShabad similarShabad = new SimilarShabad(verse.Gurmukhi);
            Navigation.PushAsync(similarShabad);
        }

        void lstShabad_ItemSelected(System.Object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            Verse verse = e.SelectedItem as Verse;
            SimilarShabad similarShabad = new SimilarShabad(verse.Gurmukhi);
            Navigation.PushAsync(similarShabad);
        }

        async void lstShabad_ItemTapped(System.Object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (ToolbarVisible)
                await EditToolbar.TranslateTo(0, 50, 100, Easing.SinOut);
            else
                await EditToolbar.TranslateTo(0, 0, 100, Easing.SinOut);
            //EditToolbar.Opacity = (EditToolbar.Opacity == 1) ? 0 : 1;
            ToolbarVisible = !ToolbarVisible;
            //if(string.IsNullOrEmpty(Title))
            //    ToggleSimilarShabadButton(false);

        }

        private void lstShabad_ItemHolding(object sender, Syncfusion.ListView.XForms.ItemHoldingEventArgs e)
        {
            if (e.ItemData == null)
                return;
            Verse verse = e.ItemData as Verse;
            ToggleSimilarShabadButton(true, verse);
        }

        private void ToggleSimilarShabadButton(bool visible, Verse verse = null)
        {
            if (visible)
            {
                itemSimilar.Text = "Find similar shabads";
                itemSimilar.IsEnabled = true;
                similarShabadVerse = verse;
                lblScripture2 = new Label();
                lblScripture2.Text = Title;
                Title = string.Empty;
                //lstShabad.SelectedItem = verse;
            }
            else
            {
                if (lblScripture2 != null && !string.IsNullOrEmpty(lblScripture2.Text))
                    Title = lblScripture2.Text;
                lblScripture2 = null;
                itemSimilar.Text = string.Empty;
            }
        }

        private void Behavior_CommandClicked(object listItem)
        {
            Verse verse = listItem as Verse;
            SimilarShabad similarShabad = new SimilarShabad(verse.Gurmukhi);
            Navigation.PushAsync(similarShabad);
        }
    }
    #endregion
}

