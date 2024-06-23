using DBTest.Models;
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
    public partial class WriterList : ContentPage
    {
        SQLiteAsyncConnection _con;
        public WriterList()
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            InitializeComponent();
            LoadWriters();
        }

        private async void LoadWriters()
        {
            List<Writer> writers = await _con.QueryAsync<Writer>(Queries.GetAllWriters());
            lstWriter.ItemsSource = writers;
        }
        protected override void OnAppearing()
        {
            Theme theme = new Theme();
            BindingContext = theme;
            base.OnAppearing();
        }

        private async void lstWriter_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            Writer writer = e.SelectedItem as Writer;
            await Navigation.PushAsync(new WriterShabadList(writer));
            lstWriter.SelectedItem = null;
        }
    }
}