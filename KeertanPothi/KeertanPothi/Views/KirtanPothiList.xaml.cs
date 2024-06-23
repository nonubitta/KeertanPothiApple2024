using DBTest.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Acr.UserDialogs;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Essentials;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KirtanPothiList : ContentPage
    {
        SQLiteAsyncConnection _con;
        ObservableCollection<Pothi> PothiObs;
        StaticText.PothiText pothiText = new StaticText.PothiText();
        public KirtanPothiList()
        {
            BindingContext = new Theme();
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            InitializeComponent();
            MessagingCenter.Subscribe<App, bool>((App)Application.Current, "PothisImported", (sender, arg) => {
                PothisCreated(arg);
            });
            LoadToolbars();
        }

        private void LoadToolbars()
        {
            ToolbarItem itemAdd = new ToolbarItem();
            itemAdd.IconImageSource = ImageSource.FromResource("KeertanPothi.images.addSmall.png");
            itemAdd.Order = ToolbarItemOrder.Primary;
            itemAdd.Clicked += AddPothi_Clicked;
            ToolbarItems.Add(itemAdd);

            ToolbarItem itemShare = new ToolbarItem();
            itemShare.IconImageSource = ImageSource.FromResource("KeertanPothi.images.actionMenu70.png");
            itemShare.Order = ToolbarItemOrder.Primary;
            itemShare.Clicked += ShareActionSheet_Clicked;
            ToolbarItems.Add(itemShare);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<App, object>(this, "PothisImported");
        }

        private async Task<bool> LoadPothis()
        {
            List<Pothi> pothis = await _con.QueryAsync<Pothi>(Queries.GetAllPothis());
            foreach (Pothi p in pothis)
            {
                List<PothiShabad> ps = await _con.QueryAsync<PothiShabad>($"Select * from PothiShabad where PothiId = {p.PothiId}");
                p.ShabadCount = ps.Count;
            }
            PothiObs = new ObservableCollection<Pothi>(pothis);
            lstPothi.ItemsSource = PothiObs;
            if(PothiObs.Count == 0)
            {
                lblNoPothi.IsVisible = true;
                lstPothi.IsVisible = false;
            }
            else
            {
                lstPothi.IsVisible = true;
                lblNoPothi.IsVisible = false;
            }
            return true;
        }

        private async void lstPothi_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            Pothi pothi = e.SelectedItem as Pothi;
            await Navigation.PushAsync(new PothiShabadList(pothi));
            lstPothi.SelectedItem = null;
        }

        protected async override void OnAppearing()
        {
            await LoadPothis();
            Theme theme = new Theme();
            PothiObs?.ToList().ForEach(a => a.PageBgTheme = theme);
            BindingContext = theme;
            lblRenameDelete.Text = pothiText.RenameDelete;
            lblNoPothi.Text = pothiText.NoPothisFound;
        }

        private async void DeletePothi_Clicked(object sender, EventArgs e)
        {
            Pothi pothi = (sender as MenuItem).CommandParameter as Pothi;
            bool answer = await DisplayAlert("Delete", $"Are you sure you want to delete {pothi.Name} ?", "Yes", "No");
            if (answer)
            {
                try
                {
                    int deletePothiShabads = await _con.ExecuteAsync(Queries.DeletePothiShabad(pothi.PothiId.Value));
                    int deleted = await _con.DeleteAsync<Pothi>(pothi.PothiId);
                    if (deleted > 0 || pothi.PothiId == null)
                    {
                        PothiObs.Remove(pothi);
                        Util.ShowRoast("Pothi deleted successfully", true);
                        await Queries.ExportPothis(PothiObs.ToList(), true);
                    }
                }
                catch (Exception ex)
                {
                    Util.ShowRoast("Failed to Delete pothi");
                }
            }
        }

        private async void AddPothi_Clicked(object sender, EventArgs e)
        {
            string pothiName = await DisplayPromptAsync(pothiText.NewPothiNameSubject, pothiText.NewPothiNameMsg);
            if (!string.IsNullOrWhiteSpace(pothiName))
            {
                Pothi pothi = new Pothi();
                pothi.Name = pothiName;
                pothi.CreatedOn = DateTime.Now;
                var cnt = await _con.InsertAsync(pothi);
                if (cnt > 0)
                {
                    PothiObs.Add(pothi);
                }
                Util.ShowRoast("Pothi added successfully", true);
                await LoadPothis();
                await Queries.ExportPothis(PothiObs.ToList(), true);
            }
        }

        private void Rename_Clicked(object sender, EventArgs e)
        {
            Pothi pothi = (sender as MenuItem).CommandParameter as Pothi;
            RenamePothi(pothi);
        }

        private async void RenamePothi(Pothi pothi)
        {
            string newPothiName = await DisplayPromptAsync("Rename Pothi", "Enter new pothi name", "Save", "Cancel", initialValue: pothi.Name);
            if(!string.IsNullOrWhiteSpace(newPothiName) && newPothiName != pothi.Name)
            {
                pothi.Name = newPothiName;
                int updated = await _con.UpdateAsync(pothi);
                if (updated > 0)
                {
                    PothiObs[PothiObs.IndexOf(pothi)].Name = newPothiName;
                }
                await Queries.ExportPothis(PothiObs.ToList(), true);
            }
        }

        private async void Export_Clicked(object sender, EventArgs e)
        {
            if(PothiObs.Count > 0)
            {
                bool exported = await Queries.ExportPothis(PothiObs.ToList());
                if (exported)
                    Util.ShowRoast("Pothi(s) exported successfully to Internal Storage/Documents/Pothis.json.");
                else
                    Util.ShowRoast("Failed to export pothis");
            }
            else
                Util.ShowRoast("No pothis to export");
        }

        private async void Import_Clicked(object sender, EventArgs e)
        {
            var customFileType =
                new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "json" } }, // or general UTType values
                });
            var pickResult = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select file"
                //,FileTypes = customFileType
            });
            if (pickResult != null)
            {
                if (pickResult.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
                {
                    var stream = await pickResult.OpenReadAsync();
                    string js = string.Empty;
                    using (StreamReader read = new StreamReader(stream))
                    {
                        js = read.ReadToEnd();
                    }
                    List<string> jsons = new List<string>();
                    jsons.Add(js);
                    if (jsons != null && jsons.Count > 0)
                    {
                        ImportPothiPopup importPothi = new ImportPothiPopup(jsons);
                        await Navigation.PushPopupAsync(importPothi);
                    }
                    else
                        Util.ShowRoast("No file found");
                }
                else
                    await DisplayAlert("Wrong file","Not a valid json file", "OK");
            }
        }

        private async void Backup_Clicked(object sender, EventArgs e)
        {
            //await DisplayAlert("Backup", "To back up your pothi, select Google Drive or any other cloud service", "OK");
            try
            {
                List<PothiShabadExt> pothis = new List<PothiShabadExt>();
                foreach (Pothi pothi in PothiObs)
                {
                    List<PothiShabad> ps = await _con.QueryAsync<PothiShabad>($"Select * from PothiShabad where PothiId = {pothi.PothiId}");
                    PothiShabadExt ext = new PothiShabadExt() { pothi = pothi, shabadList = ps };
                    pothis.Add(ext);
                }
                string json = JsonConvert.SerializeObject(pothis);
                Util.ShareFile(json,"Pothis.json", "Pothi shared from Keertan Pothi");
                Util.ShowRoast("Pothis exported successfully");
            }
            catch (Exception exception)
            {
                Util.ShowRoast("Unable to export pothis");
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Pothi pothi = (e as TappedEventArgs).Parameter as Pothi;
            await Navigation.PushAsync(new PothiShabadList(pothi));
            lstPothi.SelectedItem = null;
        }

        private async void PothisCreated(bool arg)
        {
            if (arg)
            {
                await LoadPothis();
                Util.ShowRoast("Pothi imported successfully.", true);
                await Queries.ExportPothis(PothiObs.ToList(), true);
            }
        }

        async void ShareActionSheet_Clicked(System.Object sender, System.EventArgs e)
        {
            const string export = "Export Pothis";
            const string import = "Import Pothis";
            //const string backup = "Backup";
            string action = await DisplayActionSheet("Export/Import", "Cancel", null, export, import);

            switch (action)
            {
                case export:
                    Backup_Clicked(null, null);
                    break;
                case import:
                    Import_Clicked(null, null);
                    break;
                default:
                    break;
            }
        }
    }
}