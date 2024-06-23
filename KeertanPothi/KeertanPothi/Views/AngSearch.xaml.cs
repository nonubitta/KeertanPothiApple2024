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
    public partial class AngSearch : ContentPage
    {
        public AngSearch()
        {
            InitializeComponent();
            GetAng();
        }

        private async void GetAng()
        {
            string angNoStr = await DisplayPromptAsync("Ang Number", "Enter ang number:", "OK", "Cancel", maxLength: 4, keyboard: Keyboard.Numeric);
            if (!string.IsNullOrWhiteSpace(angNoStr))
            {
                int angNo = Convert.ToInt32(angNoStr);
                await Navigation.PushAsync(new ShabadDetails(angNo));
            }
        }
    }
}