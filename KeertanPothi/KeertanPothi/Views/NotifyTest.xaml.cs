using DBTest.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotifyTest : ContentPage
    {
        SQLiteAsyncConnection _con;
        ObservableCollection<Model_Class> models = new ObservableCollection<Model_Class>();
        double lastYPos = 0;
        //SQLiteAsyncConnection _con;
        public NotifyTest()
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            InitializeComponent();
            GetData();
            for (int i=0; i<20; i++)
            {
                models.Add(new Model_Class { Description = "Description_" + i, Content = "Content_" + i});
            }
            lstWriter.ItemsSource = models;
        }

        private async void GetData()
        {
            DbVersion version = await _con.Table<DbVersion>().FirstAsync();
            lblDbVersion.Text = version.Version.ToString();
        }
    }

    public class Model_Class : INotifyPropertyChanged
    {
        public string Content { get; set; }
        public string Description { get; set; }
        
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}