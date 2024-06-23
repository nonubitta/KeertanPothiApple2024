using DBTest.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace KeertanPothi.Controls
{
    public partial class PopupHeader : ContentView
    {
        public string Title {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }
        public PopupHeader()
        {
            InitializeComponent();
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}
