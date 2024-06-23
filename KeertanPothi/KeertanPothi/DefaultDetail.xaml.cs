using KeertanPothi.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeertanPothi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefaultDetail : ContentPage
    {
        public DefaultDetail()
        {
            InitializeComponent();
            //LoadRandomShabad();
        }

        private void LoadRandomShabad()
        {
            var rand = new Random();
            //await Navigation.PushAsync(new ShabadDetails(rand.Next(20, 5540)));
        }

        private void Help_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Help());
        }

        private void Contact_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContactUs());
        }
    }
}