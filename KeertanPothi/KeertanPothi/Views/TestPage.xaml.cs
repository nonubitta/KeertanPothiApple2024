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
    public partial class TestPage : ContentPage
    {
        ObservableCollection<Album> MyImages = new ObservableCollection<Album>();
        public TestPage()
        {
            InitializeComponent();
            for(int i = 0; i<20; i++)
            {
                Album album = new Album();
                album.Image = "Image " + i.ToString();
                album.Description = "Description " + i.ToString();
                MyImages.Add(album);
            }
            this.BindingContext = this;
            lst.ItemsSource = MyImages;
        }
    }

    public class Album
    {
        public string Image { get; set; }
        public string Description { get; set; }
    }
}