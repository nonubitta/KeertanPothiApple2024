using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeertanPothi
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CopyDB : ContentPage
    {
        public CopyDB()
        {
            InitializeComponent();
        }

        public static async void CopyDBFile(bool initialLoad)
        {
            try
            {
                //UserDialogs.Instance.ShowLoading("Initializing database...");
                await Task.Delay(100);
                Util.CopyDBFile();
                    
                //Load pothis
                bool imported = await Queries.ImportPothi();
                UserDialogs.Instance.HideLoading();
            }
            catch(Exception ex)
            {
                //UserDialogs.Instance.HideLoading();
            }
        }
    }
}