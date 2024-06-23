using DBTest.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowWriterInfo : PopupPage
	{
		public string DiedText = "Died on:";
		public string JyotiJotText = "Jyoti jot:";
		public ShowWriterInfo(List<WriterInfo> winfo)
        {
            InitializeComponent();
			if (winfo != null && winfo.Count > 0)
			{
				BindingContext = winfo[0];
				if (winfo[0].IsGuru)
					lblDeath.Text = JyotiJotText;
				else
					lblDeath.Text = DiedText;
				grdInfo.IsVisible = true;
			}

		}
		private void btnClose_Clicked(object sender, EventArgs e)
		{
			Navigation.PopPopupAsync();
		}

	}
}