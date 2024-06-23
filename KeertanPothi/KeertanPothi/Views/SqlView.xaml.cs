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
    public partial class SqlView : ContentPage
    {
        SQLiteAsyncConnection _con;
        public SqlView()
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            InitializeComponent();
        }

        private async void Run_Clicked(object sender, EventArgs e)
        {
            if (btnRun.Text == "Run")
            {
                var animate = new Animation(d => edtSql.HeightRequest = d, 1, 0, Easing.SinIn);
                animate.Commit(edtSql, "Editor", 16, 500);
                btnRun.Text = "Edit Sql";
                try
                {
                    List<AllColumns> verseSearch = await _con.QueryAsync<AllColumns>(edtSql.Text);
                    lstShabad.ItemsSource = verseSearch;
                }
                catch (Exception ex)
                {
                    Util.ShowRoast(ex.Message);
                }
            }
            else
            {
                var animate = new Animation(d => edtSql.HeightRequest = d, 1, 300, Easing.SinOut);
                animate.Commit(edtSql, "Editor", 16, 500);
                btnRun.Text = "Run";
                lstShabad.ItemsSource = null;
            }
        }
    }
}