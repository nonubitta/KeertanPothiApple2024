using DBTest.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPopup : PopupPage
    {
        public delegate void SettingChangedDelegate(Util.SettingName setting, bool? value);
        public event SettingChangedDelegate SettingChangedEvent;
        bool Initializing = false;
        int gurmukhiFontSize = 0;
        int englishTranslationFontSize = 0;
        int punjabiTranslationFontSize = 0;
        int TranslitrationFontSize = 0;


        private void InvokeSettingChanged(Util.SettingName settingName, bool? value)
        {
            SettingChangedEvent?.Invoke(settingName, value);
        }
        
        public SettingsPopup(Verse verse)
        {
            BindingContext = new Theme();
            Initializing = true;
            InitializeComponent();
            swtGurmukhi.IsToggled = verse.GurmukhiVisible;
            swtTranslation.IsToggled = Util.PrefEnglishVisible;
            swtPTranslation.IsToggled = Util.PrefPunjabiVisible;
            swtTransliteration.IsToggled = Util.PrefTransliterationVisible;
            gurmukhiFontSize = verse.GurmukhiFontSize;
            englishTranslationFontSize = verse.EnglishFontSize;
            punjabiTranslationFontSize = verse.PunjabiFontSize;
            TranslitrationFontSize = verse.TransliterationFontSize;
            chbNormal.IsChecked = Util.PrefGurmukhiFontName == Util.PunjabiFontKey;
            chbHand.IsChecked = Util.PrefGurmukhiFontName == Util.HandFontKey;
            swtVishraam.IsToggled = Util.PrefShowVishraam;
            swtLadivaar.IsToggled = Util.PrefShowLadivaar;
            //PopulateThemes();
            switch (Util.PrefDarkTheme)
            {
                case Util.ThemeNameBlue:
                    rbBlue.IsChecked = true;
                    break;
                case Util.ThemeNameBlack:
                    rbGray.IsChecked = true;
                    break;
                case Util.ThemeNameFblack:
                    rbBlack.IsChecked = true;
                    break;
            }

            Initializing = false;
        }

        //private void PopulateThemes()
        //{
        //    List<KeyValue> themes = new List<KeyValue>();
        //    themes.Add(new KeyValue { Value = "Blue", Key = Util.ThemeNameBlue });
        //    themes.Add(new KeyValue { Value = "White/Blue", Key = Util.ThemeNameGray });
        //    themes.Add(new KeyValue { Value = "Black/Blue", Key = Util.ThemeNameBlack });
        //    themes.Add(new KeyValue { Value = "Full Black", Key = Util.ThemeNameFblack });
        //    pckTheme.ItemsSource = themes;

        //    KeyValue selected = themes.FirstOrDefault(a => a.Key == Util.PrefDarkTheme);
        //    if (selected != null)
        //        pckTheme.SelectedItem = selected;
        //}

        private void swtGurmukhi_Toggled(object sender, ToggledEventArgs e)
        {
            Util.PrefGurmukhiVisible = swtGurmukhi.IsToggled;
            InvokeSettingChanged(Util.SettingName.GurmukhiVisible, swtGurmukhi.IsToggled);
        }
        protected override void OnAppearing()
        {
            Theme theme = new Theme();
            BindingContext = theme;
            base.OnAppearing();
        }

        private void stpGurmukhi_ValueChanged(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            bool value = btn.CommandParameter != null ? Convert.ToBoolean(btn.CommandParameter) : true;
            if(value)
                gurmukhiFontSize += 2;
            else
                gurmukhiFontSize -= 2;

            Util.PrefGurmukhiFontSize = gurmukhiFontSize;
            InvokeSettingChanged(Util.SettingName.GurmukhiFontSize, value);
        }

        private void swtTranslation_Toggled(object sender, ToggledEventArgs e)
        {
            Util.PrefEnglishVisible = swtTranslation.IsToggled;
            InvokeSettingChanged(Util.SettingName.EngTranslationVisible, swtTranslation.IsToggled);
        }

        private void stpTranslation_ValueChanged(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            bool value = btn.CommandParameter != null ? Convert.ToBoolean(btn.CommandParameter) : true;
            if (value)
                englishTranslationFontSize += 2;
            else
                englishTranslationFontSize -= 2;

            Util.PrefEnglishFontSize = englishTranslationFontSize;
            InvokeSettingChanged(Util.SettingName.EngTranslationFontSize, value);
        }

        private void swtPTranslation_Toggled(object sender, ToggledEventArgs e)
        {
            Util.PrefPunjabiVisible = swtPTranslation.IsToggled;
            InvokeSettingChanged(Util.SettingName.PunTranslationVisible, swtPTranslation.IsToggled);
        }

        private void stpPTranslation_ValueChanged(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            bool value = btn.CommandParameter != null ? Convert.ToBoolean(btn.CommandParameter) : true;
            if (value)
                punjabiTranslationFontSize += 2;
            else
                punjabiTranslationFontSize -= 2;

            Util.PrefPunjabiFontSize = punjabiTranslationFontSize;
            InvokeSettingChanged(Util.SettingName.PunTranslationFontSize, value);
        }

        private void stpTransliteration_ValueChanged(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            bool value = btn.CommandParameter != null ? Convert.ToBoolean(btn.CommandParameter) : true;
            if (value)
                TranslitrationFontSize += 2;
            else
                TranslitrationFontSize -= 2;

            Util.PrefTransliterationFontSize = TranslitrationFontSize;
            InvokeSettingChanged(Util.SettingName.TransliterationFontSize, value);
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private void swtLadivaar_Toggled(object sender, ToggledEventArgs e)
        {
            Util.PrefShowLadivaar = swtLadivaar.IsToggled;
            InvokeSettingChanged(Util.SettingName.Ladivaar, swtLadivaar.IsToggled);
        }

        private void swtVishraam_Toggled(object sender, ToggledEventArgs e)
        {
            Util.PrefShowVishraam = swtVishraam.IsToggled;
            InvokeSettingChanged(Util.SettingName.Vishraam, swtVishraam.IsToggled);
        }

        private void chbNormal_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            chbHand.IsChecked = !chbNormal.IsChecked;
            Util.PrefGurmukhiFontName = chbNormal.IsChecked ? Util.PunjabiFontKey : Util.HandFontKey;
            InvokeSettingChanged(Util.SettingName.NormalFont, chbNormal.IsChecked);
        }

        private void chbHand_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            chbNormal.IsChecked = !chbHand.IsChecked;
            Util.PrefGurmukhiFontName = chbHand.IsChecked ? Util.HandFontKey : Util.PunjabiFontKey;
            InvokeSettingChanged(Util.SettingName.HandFont, chbHand.IsChecked);
        }

        //private void swtDark_Toggled(object sender, ToggledEventArgs e)
        //{
            //Util.PrefDarkTheme = swtDark.IsToggled;
            //BindingContext = new Theme();
            //InvokeSettingChanged(Util.SettingName.Theme, swtDark.IsToggled);
        //}

        private void swtTransliteration_Toggled(object sender, ToggledEventArgs e)
        {
            Util.PrefTransliterationVisible = swtTransliteration.IsToggled;
            InvokeSettingChanged(Util.SettingName.TransliterationVisible, swtTransliteration.IsToggled);
        }

        //private void pckTheme_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    KeyValue keyValue1 = pckTheme.SelectedItem as KeyValue;
        //    if (keyValue1 != null)
        //    {
        //        Util.PrefDarkTheme = keyValue1.Key;
        //        BindingContext = new Theme();
        //        InvokeSettingChanged(Util.SettingName.Theme, true);
        //    }
        //}
        private void rbBlue_CheckedChanged(object sender, CheckedChangedEventArgs e)         {             if (rbBlue.IsChecked)             {                 Util.PrefDarkTheme = Util.ThemeNameBlue;                 BindingContext = new Theme();                 InvokeSettingChanged(Util.SettingName.Theme, true);             }         }          private void rbGray_CheckedChanged(object sender, CheckedChangedEventArgs e)         {             if (rbGray.IsChecked)             {                 Util.PrefDarkTheme = Util.ThemeNameBlack;                 BindingContext = new Theme();                 InvokeSettingChanged(Util.SettingName.Theme, true);             }         }          private void rbBlack_CheckedChanged(object sender, CheckedChangedEventArgs e)         {             if (rbBlack.IsChecked)             {                 Util.PrefDarkTheme = Util.ThemeNameFblack;                 BindingContext = new Theme();                 InvokeSettingChanged(Util.SettingName.Theme, true);             }         } 
    }
}