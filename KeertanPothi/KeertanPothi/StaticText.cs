using DBTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeertanPothi
{
    internal class StaticText
    {
        internal class MenuText : StaticTextBase
        {
            public string MenuSearch { get; }
            public string MenuRandom { get; }
            public string MenuKeertanPothi { get; }
            public string MenuSundarGutka { get; }
            public string MenuWriterList { get; }
            public string MenuRaagList { get; }
            public string MenuHistory { get; }
            public string MenuHelpContact { get; }
            public int FontSize { get; }

            public MenuText()
            {
                if (IsPunjabi)
                {
                    MenuSearch = "ਸ਼ਬਦ ਖੋਜ";
                    MenuRandom = "ਰੈਨਡਮ ਸ਼ਬਦ";
                    MenuKeertanPothi = "ਕੀਰਤਨ ਪੋਥੀ";
                    MenuSundarGutka = "ਸੁੰਦਰ ਗੁਟਕਾ";
                    MenuWriterList = "ਲੇਖਕ ਸੂਚੀ";
                    MenuRaagList = "ਰਾਗ  ਸੂਚੀ";
                    MenuHistory = "ਖੋਜੇ ਸ਼ਬਦ";
                    MenuHelpContact = "ਮਦਦ/ਸੰਪਰਕ";
                    FontSize = 26;
                }
                else
                {
                    MenuSearch = "Search";
                    MenuRandom = "Random Shabad";
                    MenuKeertanPothi = "Keertan Pothi";
                    MenuSundarGutka = "Sundar Gutka";
                    MenuWriterList = "Writer List";
                    MenuRaagList = "Raag List";
                    MenuHistory = "History";
                    MenuHelpContact = "Help/Contact";
                    FontSize = 22;
                }
            }
        }

        internal class SearchText : StaticTextBase
        {
            public string ThreeLetterInfo { get; }
            public string HowToHelpInfo { get; }
            public string DDLFirstLetterSearch { get; }
            public string DDLMainLetterSearch { get; }
            public string DDLExactSearch { get; }
            public string DDLAngSearch { get; }
            public string NewFeatureLink { get; set; }

            public SearchText()
            {
                if (IsPunjabi)
                {
                    ThreeLetterInfo = "* ਨਤੀਜੇ ਵੇਖਣੇ ਸ਼ੁਰੂ ਕਰਨ ਲਈ 3 ਅੱਖਰ ਟਾਈਪ ਕਰੋ";
                    HowToHelpInfo = "ਕਿਸ ਤਰ੍ਹਾਂ ਖੋਜ ਕੀਤੀ ਜਾ ਸਕਦੀ ਹੈ ਦੀ ਸਹਾਇਤਾ ਲਈ ਹੇਠਾਂ ਦਿੱਤੇ 'Help' ਬਟਨ 'ਤੇ ਕਲਿਕ ਕਰੋ";
                    DDLFirstLetterSearch = "ਪਹਿਲੇ ਅਖਰ ਤੋਂ ਖੋਜ";
                    DDLMainLetterSearch = "ਬਿਨਾ ਲਗਾ ਮਾਤਰਾ ਤੋਂ ਖੋਜ";
                    DDLExactSearch = "ਲਗਾ ਮਾਤਰਾ ਨਾਲ ਖੋਜ";
                    DDLAngSearch = "ਅੰਗ ਖੋਜ";
                    NewFeatureLink = "ਨਵੀਆਂ ਵਿਸ਼ੇਸ਼ਤਾਵਾਂ ਨੂੰ ਵੇਖਣ ਲਈ ਇੱਥੇ ਕਲਿੱਕ ਕਰੋ";
                }
                else
                {
                    ThreeLetterInfo = "* Type 3 letters to start seeing results";
                    HowToHelpInfo = "Click on 'Help' button below to access help on how to search";
                    DDLFirstLetterSearch = "First Letter Search";
                    DDLMainLetterSearch = "Main Letter Search";
                    DDLExactSearch = "Exact Search";
                    DDLAngSearch = "Ang Search";
                    NewFeatureLink = "Click here to see new features";
                }
            }
        }

        internal class PothiText : StaticTextBase
        {
            public string RenameDelete { get; }
            public string AddPothi { get; set; }
            public string NoPothisFound { get; }
            public string NewPothiNameSubject { get; }
            public string NewPothiNameMsg { get; }
            public PothiText()
            {
                if (IsPunjabi)
                {
                    RenameDelete = "ਨਾਮ ਬਦਲਣ ਜਾਂ ਮਿਟਾਉਣ ਲਈ ਪੋਥੀ ਨੂੰ ਖੱਬੇ ਪਾਸੇ ਸਵਾਈਪ ਕਰੋ";
                    AddPothi = "ਨਵੀਂ ਪੋਥੀ ਬਣਾਉਣ ਲਈ," + Environment.NewLine + "ਸੱਜੇ ਤੋਂ ਉੱਪਰ ਦੇ ਪਲੱਸ (+) ਬਟਨ ਤੇ ਕਲਿਕ ਕਰੋ";
                    NoPothisFound = "ਤੁਸੀਂ ਕੋਈ ਪੋਥੀ ਨਹੀਂ ਬਣਾਈ ਹੈ|" + Environment.NewLine + AddPothi;
                    NewPothiNameSubject = "ਨਵੀਂ ਪੋਥੀ ਬਣਾਓ";
                    NewPothiNameMsg = "ਨਵੀਂ ਪੋਥੀ ਦਾ ਨਾਮ";
                }
                else
                {
                    RenameDelete = "Swipe left to rename or delete a pothi";
                    AddPothi = "Click on plus button in top right" + Environment.NewLine + "to create your first pothi.";
                    NoPothisFound = "No pothis found." + Environment.NewLine + AddPothi;
                    NewPothiNameSubject = "Add new Pothi";
                    NewPothiNameMsg = "Enter pothi name";
                }

            }
        }

        internal class Settings : StaticTextBase
        {
            public string ThemeBlue { get; set; }
            public string ThemeGray { get; set; }
            public string ThemeBlack { get; set; }
            public string ThemeFblack { get; set; }
            public Settings()
            {
                if (IsPunjabi)
                {

                }
                else
                {

                }

            }
        }

        internal class PothiShabadsText : StaticTextBase
        {
            public string NoShabadsAdded1 { get; }
            public string NoShabadsAdded2 { get; }
            public string NoShabadsAdded3 { get; }
            public string ButtonText { get; set; }

            public PothiShabadsText()
            {
                if (IsPunjabi)
                {
                    NoShabadsAdded1 = "ਤੁਸੀਂ ਇਸ ਪੋਥੀ ਵਿਚ ਕੋਈ ਸ਼ਬਦ ਸ਼ਾਮਲ ਨਹੀਂ ਕੀਤਾ ਹੈ";
                    NoShabadsAdded2 = "ਸ਼ਬਦ ਸਕ੍ਰੀਨ ਤੋਂ, ਕਲਿੱਕ ਕਰੋ";
                    NoShabadsAdded3 = "ਸ਼ਬਦ ਨੂੰ ਪੋਥੀ ਵਿਚ ਸ਼ਾਮਲ ਕਰਨ ਲਈ ";
                    ButtonText = "ਪੋਥੀ ਵਿਚ ਜੋੜਨ ਲਈ ਸ਼ਬਦ ਖੋਜ ਕਰੋ";
                }
                else
                {
                    NoShabadsAdded1 = "You haven't added any shabad to this pothi.";
                    NoShabadsAdded2 = "From shabad screen, click ";
                    NoShabadsAdded3 = "icon, to add shabad to pothi";
                    ButtonText = "Search Shabad to add to this Pothi";
                }

            }
        }

        internal class NewFeaturePopup : StaticTextBase
        {
            public List<NewFeature> newFeatures = new List<NewFeature>();
            public string Title { get; set; }
            public NewFeaturePopup()
            {
                if (IsPunjabi)
                {
                    Title = "ਨਵੀਆਂ ਵਿਸ਼ੇਸ਼ਤਾਵਾਂ";
                    newFeatures.Add(new NewFeature
                    {
                        Name = "ਪੰਜਾਬੀ ਭਾਸ਼ਾ",
                        Location = "ਖੱਬਾ ਮੀਨੂੰ",
                        Details = "ਜ਼ਿਆਦਾਤਰ ਸਕ੍ਰੀਨਾਂ ਲਈ ਪੰਜਾਬੀ ਨੂੰ ਵਿਕਲਪੀ ਭਾਸ਼ਾ ਵਜੋਂ ਸ਼ਾਮਲ ਕੀਤਾ ਗਿਆ ਹੈ",
                        ImageUrl = "images/nfpunjabi.jpg"
                    });
                    newFeatures.Add(new NewFeature
                    {
                        Name = "ਆਸਾ ਕੀ ਵਾਰ ਮੋਡ",
                        Location = "ਸੁੰਦਰ ਗੁਟਕਾ-> ਆਸਾ ਕੀ ਵਾਰ-> ਹਾਰਮੋਨੀਅਮ ਆਈਕਾਨ",
                        Details = "ਸੰਗਤ ਹੁਣ ਕੀਰਤਨ ਪੋਥੀ ਦੇ ਨਾਲ ਆਸਾ ਦੀ ਵਾਰ ਦੀ ਵਰਤੋ ਕਰ ਸਕਦੀ ਹੈ. ਉਹ ਟੈਬਸ ਤੇ ਕਲਿਕ ਕਰਕੇ ਸ਼ਬਦ ਅਤੇ ਆਸਾ ਕੀ ਵਾਰ ਇਕ ਨਾਲ ਖੋਲ ਸਕਦੇ ਹਨ",
                        ImageUrl = "images/nfakv.jpg"
                    });
                    newFeatures.Add(new NewFeature
                    {
                        Name = "ਮਿਲਦੇ ਸ਼ਬਦ ਲੱਭੋ",
                        Location = "ਸ਼ਬਦ ਸਕ੍ਰੀਨ->ਸ਼ਬਦ ਦੀ ਲਾਇਨ ਨੂੰ ਦਬਾ ਕੇ ਰਖੋ->Find similar shabads",
                        Details = "ਸੰਗਤ ਹੁਣ ਕਿਸੇ ਵੀ ਤੁਕ ਦੇ ਸਮਾਨ ਸ਼ਬਦਾਂ ਨੂੰ ਲੱਭ ਸਕਦਿਆ ਹਨ",
                        ImageUrl = "images/nfsimilar.jpg"
                    });
                }
                else
                {
                    Title = "New Features";
                    newFeatures.Add(new NewFeature
                    {
                        Name = "Punjabi Language",
                        Location = "Left Menu",
                        Details = "Punjabi has been added as alternate language for most of the screens",
                        ImageUrl = "images/nfpunjabi.jpg"
                    });
                    newFeatures.Add(new NewFeature
                    {
                        Name = "Asa ki Vaar mode",
                        Location = "Sundar Gutka->Asa ki Vaar->Harmonium icon",
                        Details = "User can now open asa ki vaar path along with keertan pothis. User can easily switch between shabads and asa ki vaar by clicking tabs",
                        ImageUrl = "images/nfakv.jpg"
                    });
                    newFeatures.Add(new NewFeature
                    {
                        Name = "Find Similar Shabads",
                        Location = "Shabad screen->Long press verse->Find similar shabads at the top",
                        Details = "User can now find similar shabads for any verse",
                        ImageUrl = "images/nfsimilar.jpg"
                    });
                }

            }
        }

        internal class ShabadsDetails : StaticTextBase
        {
            public string FindSimilarShabadsText { get; }

            public ShabadsDetails()
            {
                if (IsPunjabi)
                {
                    FindSimilarShabadsText = "ਮਿਲਦੇ ਸ਼ਬਦ ਲੱਭੋ";
                }
                else
                {
                    FindSimilarShabadsText = "Find similar shabads";
                }

            }
        }

        internal class ContactUsText : StaticTextBase
        {
            public string TitleSearchShabad { get; }
            public string FirstLetterTitle { get; set; }
            public string FirstLetterText { get; set; }

            public string MainLetterTitle { get; set; }
            public string MainLetterText { get; set; }

            public string AngSearchTitle { get; set; }
            public string AngSearchText { get; set; }

            public string CreatePothi { get; set; }
            public string CreatePothi1 { get; set; }
            public string CreatePothi2 { get; set; }
            public string CreatePothi3 { get; set; }
            public string CreatePothi4 { get; set; }
            public string CreatePothi5 { get; set; }

            public string AddShabad { get; set; }
            public string AddShabad1 { get; set; }
            public string AddShabad2 { get; set; }
            public string AddShabad4 { get; set; }

            public string SimilarShabads { get; set; }
            public string SimilarShabads1 { get; set; }
            public string SimilarShabads2 { get; set; }
            public string SimilarShabads3 { get; set; }

            public string AsaKiVaarMode { get; set; }
            public string AsaKiVaarMode1 { get; set; }
            public string AsaKiVaarMode2 { get; set; }
            public string AsaKiVaarMode3 { get; set; }

            public ContactUsText()
            {
                if (IsPunjabi)
                {
                    TitleSearchShabad = "1. ਸ਼ਬਦ ਲੱਭਣਾ:";

                    FirstLetterTitle = "ਪਹਿਲੇ ਅਖਰ ਤੋਂ ਖੋਜ: ";
                    FirstLetterText = "ਹੇਠਾਂ ਦਿੱਤੀ ਸੂਚੀ ਵਿੱਚੋਂ 'ਪਹਿਲੇ ਅਖਰ ਤੋਂ ਖੋਜ' ਦੀ ਚੋਣ ਕਰੋ." + Environment.NewLine + "ਹਰੇਕ ਸ਼ਬਦ ਦਾ ਪਹਿਲਾ ਅੱਖਰ ਟਾਈਪ ਕਰੋ|" + Environment.NewLine + "ਉਦਾਹਰਣ ਦੇ ਲਈ:";
                    MainLetterTitle = "ਬਿਨਾ ਲਗਾ ਮਾਤਰਾ ਤੋਂ ਖੋਜ: ";
                    MainLetterText = "ਹੇਠਾਂ ਦਿੱਤੀ ਸੂਚੀ ਵਿੱਚੋਂ 'ਬਿਨਾ ਲਗਾ ਮਾਤਰਾ ਤੋਂ ਖੋਜ' ਦੀ ਚੋਣ ਕਰੋ." + Environment.NewLine + "ਹਰੇਕ ਅੱਖਰ ਬਿਨਾ ਲਗਾ ਮਾਤਰਾ ਤੋਂ ਟਾਈਪ ਕਰੋ|" + Environment.NewLine + "ਉਦਾਹਰਣ ਦੇ ਲਈ:";
                    AngSearchTitle = "ਅੰਗ ਖੋਜ: ";
                    AngSearchText = "ਹੇਠਾਂ ਦਿੱਤੀ ਸੂਚੀ ਵਿੱਚੋਂ 'ਅੰਗ ਖੋਜ' ਦੀ ਚੋਣ ਕਰੋ." + Environment.NewLine + "ਫਿਰ ਅੰਗ ਨੰਬਰ ਟਾਈਪ ਕਰੋ.";

                    CreatePothi = "2. ਨਵੀਂ ਪੋਥੀ: ";
                    CreatePothi1 = "ਨਵੀਂ ਪੋਥੀ ਬਣਾਓਣ ਲਈ";
                    CreatePothi2 = "ਖੱਬੇ ਸੂਚੀ ਤੋਂ ਕੀਰਤਨ ਪੋਥੀ ਤੇ ਕਲਿਕ ਕਰੋ";
                    CreatePothi3 = "ਸਕ੍ਰੀਨ ਦੇ ਉੱਪਰ ਸੱਜੇ ਪਾਸੇ ਪਲੱਸ ਆਈਕਨ ਤੇ ਕਲਿਕ ਕਰੋ.";
                    CreatePothi4 = "ਨਵੀਂ ਪੋਥੀ ਲਈ ਨਾਮ ਦਰਜ ਕਰੋ";
                    CreatePothi5 = "";

                    AddShabad = "3. ਪੋਥੀ ਵਿਚ ਸ਼ਬਦ ਜੋੜਨ ਲਈ: ";
                    AddShabad1 = "ਸ਼ਬਦ ਸਕ੍ਰੀਨ ਤੇ ਜਾਣ ਲਈ ਸ਼ਬਦ ਦੀ ਖੋਜ ਕਰੋ ਜਾਂ ਰੈਨਡਮ ਸ਼ਬਦ ਤੇ ਕਲਿਕ ਕਰੋ";
                    AddShabad2 = "ਸ਼ਬਦ ਸਕ੍ਰੀਨ ਤੋਂ, ਕਿਤਾਬ ਆਈਕਨ ਤੇ ਕਲਿਕ ਕਰੋ";
                    AddShabad4 = "ਸ਼ਬਦ ਨੂੰ ਜੋੜਨ ਲਈ ਪੋਥੀ ਦੀ ਚੋਣ ਕਰੋ";

                    SimilarShabads = "4. ਮਿਲਦੇ ਸ਼ਬਦ ਲੱਭੋ: ";
                    SimilarShabads1 = "ਸ਼ਬਦ ਸਕ੍ਰੀਨ ਤੇ ਜਾਣ ਲਈ ਸ਼ਬਦ ਦੀ ਖੋਜ ਕਰੋ ਜਾਂ ਰੈਨਡਮ ਸ਼ਬਦ ਤੇ ਕਲਿਕ ਕਰੋ";
                    SimilarShabads2 = "ਉੱਪਰ ਸੱਜੇ ਕੋਨੇ ਤੋਂ ਤਿੰਨ ਬਿੰਦੀਆਂ ਤੇ ਕਲਿਕ ਕਰੋ ਅਤੇ ਫਿਰ 'Find similar shabads' ਤੇ ਕਲਿਕ ਕਰੋ.";
                    SimilarShabads3 = "ਉਹ ਸ਼ਬਦ ਦਬਾਓ ਜਿਸ ਦੀ ਤੁਸੀਂ ਖੋਜ ਕਰਨਾ ਚਾਹੁੰਦੇ ਹੋ";

                    AsaKiVaarMode = "5. ਆਸਾ ਕੀ ਵਾਰ ਮੋਡ:";
                    AsaKiVaarMode1 = "ਖੱਬੇ ਸੂਚੀ ਤੋਂ 'ਸੁੰਦਰ ਗੁਟਕਾ' ਤੇ ਕਲਿੱਕ ਕਰੋ";
                    AsaKiVaarMode2 = "ਬਾਣੀ ਸੂਚੀ ਤੋਂ ਆਸਾ ਦੀ ਵਾਰ ਤੇ ਕਲਿੱਕ ਕਰੋ";
                    AsaKiVaarMode3 = "ਸਕ੍ਰੀਨ ਦੇ ਉੱਪਰ ਸੱਜੇ ਪਾਸੇ ਹਾਰਮੋਨੀਅਮ ਆਈਕਾਨ ਤੇ ਕਲਿੱਕ ਕਰੋ";
                }
                else
                {
                    TitleSearchShabad = "1. Searching Shabad:";
                    FirstLetterTitle = "First letter search: ";
                    FirstLetterText = "Select First letter search from the list at the bottom." + Environment.NewLine +
                        "Then type first letter of each word." + Environment.NewLine +
                        "For example, to find shabad";
                    MainLetterTitle = "Main letter search: ";
                    MainLetterText = "Select Main letter search from the list at the bottom." + Environment.NewLine +
                    "Then type all the main letters of each word leaving matras." + Environment.NewLine +
                    "For example, To find shabad";
                    AngSearchTitle = "Ang search: ";
                    AngSearchText = "Select 'Ang Search' from the list at the bottom." + Environment.NewLine +
                    "Then type the ang number.";

                    CreatePothi = "2. Creating Pothi:";
                    CreatePothi1 = "Create new pothi: ";
                    CreatePothi2 = "Click on Keertan pothi from Menu";
                    CreatePothi3 = "Click on the plus icon in top right of the screen.";
                    CreatePothi4 = "Enter a name for new pothi";
                    CreatePothi5 = "You should see new pothi created";

                    AddShabad = "3. Adding shabad to pothi:";
                    AddShabad1 = "Search Shabad or Click on Random Shabad to go to the Shabad screen";
                    AddShabad2 = "From Shabad screen, click on the add to pothi icon";
                    AddShabad4 = "Select which pothi to add shabad to";

                    SimilarShabads = "4. Find similar shabads:";
                    SimilarShabads1 = "Search Shabad or Click on Random Shabad to go to the Shabad screen";
                    SimilarShabads2 = "Click on three dots from the top right corner and select 'Find similar shabads'";
                    SimilarShabads3 = "On 'Find similar shabads' screen, tap keywords to search for.";


                    AsaKiVaarMode = "5. Asa ki Vaar mode:";
                    AsaKiVaarMode1 = "Select 'Sundar Gutka' from the left menu";
                    AsaKiVaarMode2 = "Select 'Asa ki vaar' from the list";
                    AsaKiVaarMode3 = "Click on Harmonium icon at the top";
                }
            }
        }

        internal abstract class StaticTextBase
        {
            public bool IsEnglish { get; set; }
            public bool IsPunjabi { get; set; }
            public StaticTextBase()
            {
                string language = Util.PrefSelectedLanguage;
                switch (language)
                {
                    case "E":
                        IsEnglish = true;
                        IsPunjabi = false;
                        break;
                    case "P":
                        IsEnglish = false;
                        IsPunjabi = true;
                        break;
                    default:
                        IsEnglish = true;
                        IsPunjabi = false;
                        break;
                }
            }
        }
    }
}
