using Acr.UserDialogs;
using DBTest.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPopup : PopupPage
    {
        SQLiteAsyncConnection _con;
        public int PothiId { get; set; }
        public int ShabadId { get; set; }
        public string Notes { get; set; }
        public NotesPopup(int pothiId, int shabadId, string note)
        {
            InitializeComponent();
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            PothiId = pothiId;
            ShabadId = shabadId;
            Notes = note;
            editNote.Text = Notes;
        }
        protected override void OnAppearing()
        {
            Theme theme = new Theme();
            BindingContext = theme;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send<App, string>((App)Application.Current, "NoteUpdated", editNote.Text);
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Saving Note...");
            string query = Queries.UpdateNotes(PothiId, ShabadId, editNote.Text);
            int updated = await _con.ExecuteAsync(query);
            await Navigation.PopPopupAsync();
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Delete Note", "Are you sure you want to delete this note", "Yes", "No"))
            {
                UserDialogs.Instance.ShowLoading("Deleting Note...");
                editNote.Text = string.Empty;
                string query = Queries.UpdateNotes(PothiId, ShabadId, null);
                int updated = await _con.ExecuteAsync(query);
                await Navigation.PopPopupAsync();
            }
        }

        private void btnClose_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}