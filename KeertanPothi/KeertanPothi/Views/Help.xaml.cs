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
    public partial class Help : ContentPage
    {

        ObservableCollection<HelpItem> models = new ObservableCollection<HelpItem>();
        public Help()
        {
            InitializeComponent();
            models.Add(new HelpItem("images/help/AddToPothi.jpg", "To add new Pothi: &#10;On Keertan pothi page click 'Add new pothi'"));
            models.Add(new HelpItem("images/help/ShabadEdit.jpg", "Clik Edit to Move, copy, re-arrange shabads"));
            models.Add(new HelpItem("images/help/ShabadMove.jpg", "Move, copy shabads to other pothis"));

            crvData.ItemsSource = models;
        }
    }

    public class HelpItem
    {
        public HelpItem(string source, string content)
        {
            ImgSource = source;
            ImgText = content;
        }
        public ImageSource ImgSource { get; set; }
        public string ImgText { get; set; }
    }
}