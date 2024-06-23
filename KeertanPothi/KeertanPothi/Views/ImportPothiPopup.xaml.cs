using Acr.UserDialogs;
using DBTest.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImportPothiPopup : PopupPage
    {
        SQLiteAsyncConnection _con;
        ObservableCollection<PothiShabadExt> PothiObs;
        public delegate void BookmarkSelectedDelegate(int veresId);
        public event BookmarkSelectedDelegate BookmarkSelectedEvent;
        List<string> Json = new List<string>();
        bool imported = false;
        private void InvokeBookmarkSelectedChanged(int veresId)
        {
            BookmarkSelectedEvent?.Invoke(veresId);
        }
        public ImportPothiPopup(List<string> json)
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            BindingContext = new Theme();
            InitializeComponent();
            Json = json;
            LoadPothis();
        }

        private async void LoadPothis()
        {
            using (UserDialogs.Instance.Loading("Fetching pothi(s)..."))
            {
                await Task.Delay(100);
                List<PothiShabadExt> pothis = new List<PothiShabadExt>();
                foreach (string js in Json)
                {
                    List<PothiShabadExt> pothiFile = JsonConvert.DeserializeObject<List<PothiShabadExt>>(js);
                    pothis.AddRange(pothiFile);
                }

                if (pothis.Count > 0)
                {
                    foreach(PothiShabadExt ext in pothis)
                    {
                        ext.shabadCount = ext.shabadList.Count;
                        ext.pothi.IsChecked = true;
                    }
                    PothiObs = new ObservableCollection<PothiShabadExt>(pothis);
                    lstPothi.ItemsSource = PothiObs;
                }
            }
        }

        protected void SavePothis()
        {
            //foreach (PothiShabadExt cur in pothis)
            //{
            //    Pothi pothi = new Pothi();
            //    pothi.Name = cur.pothi.Name;
            //    pothi.CreatedOn = DateTime.Now;
            //    int cnt = await _con.InsertAsync(pothi);
            //    if (cnt > 0)
            //    {
            //        int pothiId = await Queries.GetLastId();
            //        foreach (PothiShabad shabad in cur.shabadList)
            //        {
            //            PothiShabad pothiShabad = shabad;
            //            pothiShabad.PothiId = pothiId;
            //            await _con.InsertAsync(pothiShabad);
            //        }
            //    }
            //}
        }

        protected override void OnAppearing()
        {
            Theme theme = new Theme();
            BindingContext = theme;
            base.OnAppearing();
        }
        
        private void btnSave_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        private async void btnImport_Clicked(object sender, EventArgs e)
        {
            foreach(PothiShabadExt ext in PothiObs)
            {
                if (ext.pothi.IsChecked)
                {
                    imported = true;
                    await Queries.SavePothFromPothiShabadExt(ext);
                }
            }
            await Navigation.PopPopupAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Send<App, bool>((App)Application.Current, "PothisImported", imported);
        }
    }
}