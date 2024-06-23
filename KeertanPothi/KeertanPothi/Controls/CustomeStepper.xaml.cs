using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeertanPothi.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomeStepper : ContentView
    {
        public CustomeStepper()
        {
            InitializeComponent();
            //CurVal = currentValue;
        }

        public int CurVal { get; set; } = 0;
        public int MinVal { get; set; } = 0;
        public int? MaxVal { get; set; }

        public double ButtonWidth {
            get
            {
                return minus.WidthRequest;
            }
            set
            {
                minus.WidthRequest = value;
                plus.WidthRequest = value;
            }
        }
        public double ButtonHeight
        {
            get
            {
                return minus.HeightRequest;
            }
            set
            {
                minus.HeightRequest = value;
                plus.HeightRequest = value;
            }
        }
        private void minus_Clicked(object sender, EventArgs e)
        {
            if(CurVal >= MinVal + 2)
                CurVal -= 2;
        }

        private void plus_Clicked(object sender, EventArgs e)
        {
            if (MaxVal == null || CurVal <= MaxVal - 2)
                CurVal += 2;
        }
    }
}