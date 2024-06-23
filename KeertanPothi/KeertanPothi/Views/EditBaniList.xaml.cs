using DBTest.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditBaniList : PopupPage
    {
        SQLiteAsyncConnection _con;
        ObservableCollection<NitnemBani> nitnemListOrb = new ObservableCollection<NitnemBani>();
        public EditBaniList()
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            BindingContext = new Theme();
            InitializeComponent();
            BindingContext = new Theme();
            LoadBaniList();
        }

        private async void LoadBaniList()
        {
            List<NitnemBani> banis = await _con.QueryAsync<NitnemBani>(Queries.GetAllBanis(true));
            nitnemListOrb = new ObservableCollection<NitnemBani>(banis);
            lstNitnem.ItemsSource = nitnemListOrb;
        }

        private async void AddBani(NitnemBani bani)
        {
            NitnemBani selectedBani = nitnemListOrb.FirstOrDefault(a => a.Id == bani.Id);
            selectedBani.IsVisible = true;
            int cnt = await _con.ExecuteAsync(Queries.ToggleNitnemBani(selectedBani.Id, true));
        }

        private async void RemoveBani(NitnemBani bani)
        {
            NitnemBani selectedBani = nitnemListOrb.FirstOrDefault(a => a.Id == bani.Id);
            selectedBani.IsVisible = false;
            int cnt = await _con.ExecuteAsync(Queries.ToggleNitnemBani(selectedBani.Id, false));
        }
        private void btnClose_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Send<App>((App)Application.Current, "BaniListUpdated");
        }

        private void Bani_Toggled(object sender, ToggledEventArgs e)
        {
            var swt = (Switch)sender;
            if (!swt.IsEnabled)
            {
                swt.IsEnabled = true;
                return;
            }
            NitnemBani selected = swt.BindingContext as NitnemBani;
            if (swt.IsToggled)
                AddBani(selected);
            else
                RemoveBani(selected);
        }

        private async void AddAll_Clicked(object sender, EventArgs e)
        {
            nitnemListOrb.ToList().ForEach(a=>a.IsVisible = true);
            int cnt = await _con.ExecuteAsync(Queries.ToggleNitnemBani(null, true));
        }

        private async void RemoveAll_Clicked(object sender, EventArgs e)
        {
            nitnemListOrb.ToList().ForEach(a => a.IsVisible = false);
            int cnt = await _con.ExecuteAsync(Queries.ToggleNitnemBani(null, false));
        }
    }
}