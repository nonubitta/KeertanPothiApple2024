using KeertanPothi;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace DBTest.Models
{
    public class Writer : ModalBase
    {
        public int? WriterID { get; set; }

        public string WriterEnglish { get; set; }

        public string WriterGurmukhi { get; set; }
    }

    public class Source : ModalBase
    {
        public int recid { get; set; }
        public int UniqueID { get; set; }
        public string SourceID { get; set; }
        public string SourceGurmukhi { get; set; }
        public string SourceUnicode { get; set; }
        public string SourceEnglish { get; set; }
    }

    public class Raag : ModalBase
    {
        public int recid { get; set; }
        public int? RaagID { get; set; }
        public string RaagGurmukhi { get; set; }
        public string RaagUnicode { get; set; }
        public string RaagEnglish { get; set; }
        public int? StartID { get; set; }
        public int? EndID { get; set; }
        public string RaagWithPage { get; set; }
    }

    public class Shabad : ModalBase
    {
        public int recid { get; set; }
        public int VerseID { get; set; }
        public int ShabadID { get; set; }
        public string Updated { get; set; }
    }

    public class Verse : ModalBase
    {
        public Verse()
        {
            gurmukhiVisible = Util.PrefGurmukhiVisible;
            translationVisible = Util.PrefEnglishVisible;
            punjabiTranslationVisible = Util.PrefPunjabiVisible;
            gurmukhiFontSize = Util.PrefGurmukhiFontSize;
            englishFontSize = Util.PrefEnglishFontSize;
            punjabiFontSize = Util.PrefPunjabiFontSize;
            gurmukhiFontName = Util.PrefGurmukhiFontName;
            transliterationVisible = Util.PrefTransliterationVisible;
            transliterationFontSize = Util.PrefTransliterationFontSize;
        }

        public int recid { get; set; }
        public int ID { get; set; }
        public string English { get; set; }
        public string Gurmukhi { get; set; }

        private string gurmukhiHtml;
        public string GurmukhiHtml
        {
            get
            {
                return gurmukhiHtml;
            }
            set
            {
                if (gurmukhiHtml != value)
                {
                    gurmukhiHtml = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string GurmukhiBisram { get; set; }
        public string GurmukhiUni { get; set; }
        public string Translations { get; set; }
        public string Transliterations { get; set; }
        public int? WriterID { get; set; }
        public string Punjabi { get; set; }
        public string PunjabiUni { get; set; }
        public string Spanish { get; set; }
        public int? RaagID { get; set; }
        public int? PageNo { get; set; }
        public int? LineNo { get; set; }
        public string SourceID { get; set; }
        public string FirstLetterStr { get; set; }
        public string MainLetters { get; set; }
        public string Bisram { get; set; }
        public string Visraam { get; set; }
        public string igurbani_bisram1 { get; set; }
        public string igurbani_bisram2 { get; set; }
        public string FirstLetterEng { get; set; }
        public string Transliteration { get; set; }
        public int? FirstLetterLen { get; set; }
        public string SourceEnglish { get; set; }
        public string WriterEnglish { get; set; }
        public string RaagEnglish { get; set; }

        private string listBgColor { get; set; }
        public string ListBgColor
        {
            get
            {
                return listBgColor;
            }
            set
            {
                if (listBgColor != value)
                {
                    listBgColor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool gurmukhiVisible;
        public bool GurmukhiVisible
        {
            get
            {
                return gurmukhiVisible;
            }
            set
            {
                if (gurmukhiVisible != value)
                {
                    gurmukhiVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool hindiVisible;
        public bool HindiVisible
        {
            get
            {
                return hindiVisible;
            }
            set
            {
                if (hindiVisible != value)
                {
                    hindiVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool translationVisible;
        public bool TranslationVisible
        {
            get
            {
                return translationVisible && (!string.IsNullOrEmpty(English));
            }
            set
            {
                if (translationVisible != value)
                {
                    translationVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool transliterationVisible;
        public bool TransliterationVisible
        {
            get
            {
                return transliterationVisible && (!string.IsNullOrEmpty(Transliteration));
            }
            set
            {
                if (transliterationVisible != value)
                {
                    transliterationVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool punjabiTranslationVisible;
        public bool PunjabiTranslationVisible
        {
            get
            {
                return punjabiTranslationVisible && (!string.IsNullOrEmpty(Punjabi));
            }
            set
            {
                if (punjabiTranslationVisible != value)
                {
                    punjabiTranslationVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int gurmukhiFontSize;
        public int GurmukhiFontSize
        {
            get
            {
                return gurmukhiFontSize;
            }
            set
            {
                if (gurmukhiFontSize != value)
                {
                    gurmukhiFontSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int hindiFontSize;
        public int HindiFontSize
        {
            get
            {
                return hindiFontSize;
            }
            set
            {
                if (hindiFontSize != value)
                {
                    hindiFontSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int englishFontSize;
        public int EnglishFontSize
        {
            get
            {
                return englishFontSize;
            }
            set
            {
                if (englishFontSize != value)
                {
                    englishFontSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int punjabiFontSize;
        public int PunjabiFontSize
        {
            get
            {
                return punjabiFontSize;
            }
            set
            {
                if (punjabiFontSize != value)
                {
                    punjabiFontSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int transliterationFontSize;
        public int TransliterationFontSize
        {
            get
            {
                return transliterationFontSize;
            }
            set
            {
                if (transliterationFontSize != value)
                {
                    transliterationFontSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string gurmukhiFontName;
        public string GurmukhiFontName
        {
            get
            {
                return gurmukhiFontName;
            }
            set
            {
                if (gurmukhiFontName != value)
                {
                    gurmukhiFontName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string updated;
        public string Updated
        {
            get
            {
                return updated;
            }
            set
            {
                if (updated != value)
                {
                    updated = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private TextAlignment textAlign;
        public TextAlignment TextAlign 
        { 
            get { if (WriterID == null) return TextAlignment.Center; else return TextAlignment.Start; }
            set
            {
                if (textAlign != value)
                {
                    textAlign = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }

    public class Pothi : ModalBase
    {
        [PrimaryKey]
        public int? PothiId { get; set; }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DateTime CreatedOn { get; set; }

        [Ignore]
        public int ShabadCount { get; set; }

        [Ignore]
        public int MaxSortOrder { get; set; }
    }

    public class PothiShabad : ModalBase
    {
        public int PothiId { get; set; }

        public int ShabadId { get; set; }

        public int? VerseId { get; set; }

        public int? SortOrder { get; set; }

        public string Notes { get; set; }

    }

    public class PothiShabadExt
    {
        public Pothi pothi { get; set; }
        public List<PothiShabad> shabadList { get; set; }

        public int shabadCount { get; set; }
    }

    public class Words : ModalBase
    {
        public Words(string item)
        {
            Item = item;
        }
        public string Item { get; set; }
    }

    public class VerseSearch : ModalBase
    {

        public int VerseID { get; set; }
        public string Gurmukhi { get; set; }
        public string English { get; set; }
        public string WriterEnglish { get; set; }
        public string RaagEnglish { get; set; }
        public int? PageNo { get; set; }
        public int? ShabadId { get; set; }
        public string Notes { get; set; }

        private int? sortOrder;
        public int? SortOrder 
        {
            get
            {
                return sortOrder;
            }
            set
            {
                if (sortOrder != value)
                {
                    sortOrder = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public DateTime? CreatedOn { get; set; }
    }

    public class WriterInfo : ModalBase
    {
        public int WriterId { get; set; }
        public string BirthDt { get; set; }
        public DateTime? BirthDtConv
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(BirthDt))
                    return DateTime.Parse(BirthDt);
                else
                    return null;
            }
        }
        public string BirthPlace { get; set; }
        public string DeathDt { get; set; }
        public DateTime? DeathDtConv
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(DeathDt))
                    return DateTime.Parse(DeathDt);
                else
                    return null;
            }
        }
        public string Spouse { get; set; }
        public string Children { get; set; }
        public string Parents { get; set; }
        public bool IsGuru { get; set; }
        public string Info { get; set; }
    }

    public class Model : INotifyPropertyChanged
    {
        private string content;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                if (content != value)
                {
                    content = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                {
                    description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    NotifyPropertyChanged();
                }
            }
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class VerseExt : INotifyPropertyChanged
    {
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public VerseExt()
        {
            gurmukhiFontSize = 16;
            englishTranslationFontSize = 24;
            punjabiTranslationFontSize = 24;
        }
        public ObservableCollection<Verse> VerseObj { get; set; }

        private int gurmukhiFontSize;
        public int GurmukhiFontSize
        {
            get
            { return gurmukhiFontSize; }
            set
            {
                if (gurmukhiFontSize != value)
                {
                    gurmukhiFontSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int englishTranslationFontSize;
        public int EnglishTranslationFontSize
        {
            get
            { return englishTranslationFontSize; }
            set
            {
                if (englishTranslationFontSize != value)
                {
                    englishTranslationFontSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int punjabiTranslationFontSize;
        public int PunjabiTranslationFontSize
        {
            get
            { return punjabiTranslationFontSize; }
            set
            {
                if (punjabiTranslationFontSize != value)
                {
                    punjabiTranslationFontSize = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }

    public class Vishraam
    {
        public string sttm { get; set; }
        public string igurbani { get; set; }
    }

    public class AllColumns
    {
        public int? WriterID { get; set; }
        public string WriterEnglish { get; set; }
        public string WriterGurmukhi { get; set; }

        public string SourceID { get; set; }
        public string SourceUnicode { get; set; }
        public string SourceEnglish { get; set; }

        public string RaagUnicode { get; set; }
        public string RaagEnglish { get; set; }

        public int VerseID { get; set; }
        public int ShabadID { get; set; }

        public int PothiId { get; set; }
        public int Name { get; set; }

        public int ID { get; set; }
        public string English { get; set; }
        public string Gurmukhi { get; set; }

    }

    public class History : ModalBase
    {
        public History(int shabadId, int? verseId)
        {
            ShabadID = shabadId;
            VerseID = verseId;
            CreatedOn = DateTime.Now;
        }
        public int ShabadID { get; set; }
        public int? VerseID { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class NewFeature
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
    }

    [Serializable]
    public class OpenLastShabad
    {
        public string RequestFrom { get; set; } = string.Empty;
        public int? ShabadID { get; set; }
        public int? VerseID { get; set; }
        public int? PothiId { get; set; }
        public int? AngNo { get; set; }
    }

    public class Theme : INotifyPropertyChanged
    {
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public string PageBg { get; set; }
        public string FontColor { get; set; }
        public string PunjabiTranslationColor { get; set; }
        public string EnglishTranslationColor { get; set; }
        public string EnglishTransliterationColor { get; set; }
        public string DefaultItemBg { get; set; }
        public string HeaderColor { get; set; }
        public string selectedItemBg;
        public string SelectedItemBg
        {
            get
            {
                return selectedItemBg;
            }
            set
            {
                if (selectedItemBg != value)
                {
                    selectedItemBg = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Theme()
        {
            switch(Util.PrefDarkTheme)
            {
                case Util.ThemeNameBlack:
                    PageBg = Util.DarkThemeBgColor;
                    FontColor = Util.DarkThemeFontColor;
                    DefaultItemBg = Util.DarkThemeSelectedItemBg;
                    PunjabiTranslationColor = Util.DarkThemePunjabiTranlationColor;
                    EnglishTranslationColor = Util.DarkThemeEnglishTranlationColor;
                    EnglishTransliterationColor = Util.DarkThemeEnglishTransliterationColor;
                    HeaderColor = Util.RegularHeaderColor;
                    break;

                case Util.ThemeNameGray:
                    PageBg = Util.GrayThemeBgColor;
                    FontColor = Util.LightThemeFontColor;
                    DefaultItemBg = Util.LightThemeSelectedItemBg;
                    PunjabiTranslationColor = Util.LightThemePunjabiTranlationColor;
                    EnglishTranslationColor = Util.LightThemeEnglishTranlationColor;
                    EnglishTransliterationColor = Util.LightThemeEnglishTransliterationColor;
                    HeaderColor = Util.RegularHeaderColor;
                    break;

                case Util.ThemeNameBlue:
                    PageBg = Util.LightThemeBgColor;
                    FontColor = Util.LightThemeFontColor; 
                    DefaultItemBg = Util.LightThemeSelectedItemBg;
                    PunjabiTranslationColor = Util.LightThemePunjabiTranlationColor;
                    EnglishTranslationColor = Util.LightThemeEnglishTranlationColor;
                    EnglishTransliterationColor = Util.LightThemeEnglishTransliterationColor;
                    HeaderColor = Util.RegularHeaderColor;
                    break;

                case Util.ThemeNameFblack:
                    PageBg = Util.DarkThemeBgColor;
                    FontColor = Util.DarkThemeFontColor;
                    DefaultItemBg = Util.DarkThemeSelectedItemBg;
                    PunjabiTranslationColor = Util.DarkThemePunjabiTranlationColor;
                    EnglishTranslationColor = Util.DarkThemeEnglishTranlationColor;
                    EnglishTransliterationColor = Util.DarkThemeEnglishTransliterationColor;
                    HeaderColor = Util.FullBlackHeaderColor;
                    break;
            }
        }
    }
   
    public class NitnemBani : ModalBase
    {
        [PrimaryKey]
        public int Id { get; set; }
        [Ignore]
        public int BaniId { get { return Id; } set { Id = value; } }
        public string PunjabiName { get; set; }
        public string EnglishName { get; set; }
        public bool Bookmark { get; set; }

        [Ignore]
        public string ShabadList { get; set; }

        [Ignore]
        public bool IsSingleBani { get; set; }

        private bool isVisible;
        public bool IsVisible
        {
            get
            { return isVisible; }
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Ignore]
        public bool IsNotVisible 
        { 
            get { return !IsVisible; } 
            set { NotifyPropertyChanged(); } 
        }
    }

    public class DbVersion
    {
        public int Version { get; set; }
    }

    public class ModalBase : INotifyPropertyChanged
    {
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ModalBase()
        {
            IsChecked = false;
            IsEditable = false;
            pageBgTheme = new Theme();
        }
        [Ignore]
        public bool IsChecked { get; set; }

        private bool isEditable;
        [Ignore]
        public bool IsEditable
        {
            get
            { return isEditable; }
            set
            {
                if (isEditable != value)
                {
                    isEditable = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Theme pageBgTheme { get; set; }
        [Ignore]
        public Theme PageBgTheme
        {
            get
            {
                return pageBgTheme;
            }
            set
            {
                if (pageBgTheme != value)
                {
                    pageBgTheme = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
