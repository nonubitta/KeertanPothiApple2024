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
using Xamarin.Essentials;
using Newtonsoft.Json.Linq;

namespace KeertanPothi.Views
{
    public partial class ShareShabadPopup : PopupPage
    {
        ObservableCollection<Verse> versesObs = new ObservableCollection<Verse>();
        int ShabadId = 0;

        public ShareShabadPopup(ObservableCollection<Verse> verses, int shabadId)
        {
            InitializeComponent();
            versesObs = verses;
            ShabadId = shabadId;
        }

        void TextShareClicked()
        {
            string str = $"Shared from Keertan Pothi(Id:{ShabadId.ToString()})\r\n";
            foreach (Verse verse in versesObs)
            {
                str += verse.GurmukhiUni + "\r\n";
            }
            Util.ShareText(str);
        }

        void FileShareClicked()
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

        protected override void OnAppearing()
        {
            Theme theme = new Theme();
            BindingContext = theme;
            base.OnAppearing();
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

        async void CopyClipboardClicked()
        {
            string str = string.Empty;
            foreach (Verse verse in versesObs)
            {
                str += verse.GurmukhiUni + "\r\n";
            }
            await Clipboard.SetTextAsync(str);
        }

        private void btnClose_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private void lstMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            switch (e.SelectedItemIndex)
            {
                case 0: TextShareClicked();
                    break;
                case 1: FileShareClicked();
                    break;
                case 2: CopyClipboardClicked();
                    break;
                default: TextShareClicked();
                    break;
            }
            //VerseSearch verse = e.SelectedItem as VerseSearch;
            //Shabad shabad = await _con.Table<Shabad>().FirstOrDefaultAsync(a => a.VerseID == verse.VerseID);
            //await Navigation.PushAsync(new ShabadDetails(shabad.ShabadID, verse.VerseID));
            //lstVerse.SelectedItem = null;
        }
    }
}
