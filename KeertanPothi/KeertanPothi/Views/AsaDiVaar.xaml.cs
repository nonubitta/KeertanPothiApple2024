using Acr.UserDialogs;
using DBTest.Models;
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
    public partial class AsaDiVaar : TabbedPage
    {
        public AsaDiVaar()
        {
            InitializeComponent();
            LoadTabs();
        }

        private async void LoadTabs()
        {
            using (UserDialogs.Instance.Loading("Loading Asa ki Vaar keertan mode..."))
            {
                await Task.Delay(300);
                NitnemBani bani = new NitnemBani();
                bani.BaniId = 18;
                bani.EnglishName = "Asa ki Vaar";
                bani.PunjabiName = "Awsw kI vwr";
                this.Children.Add(new ShabadDetailsLight(bani) { Title = "Asa ki Vaar" });

                this.Children.Add(new NavigationPage(new KirtanPothiList()) { Title = "Pothi Shabad" });
            }
        }
    }
}