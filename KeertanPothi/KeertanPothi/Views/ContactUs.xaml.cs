using DBTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeertanPothi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactUs : TabbedPage
    {
        public ContactUs()
        {
            BindingContext = new Theme();
            InitializeComponent();
            StaticText.ContactUsText contactUsText = new StaticText.ContactUsText();
            lblTitleSearchShabad.Text = contactUsText.TitleSearchShabad;
            lblFirstLetterSearch.Text = contactUsText.FirstLetterTitle;
            lblFirstLetterText.Text = contactUsText.FirstLetterText;

            lblMainLetter1.Text = contactUsText.MainLetterTitle;
            lblMainLetter2.Text = contactUsText.MainLetterText;

            lblAng1.Text = contactUsText.AngSearchTitle;
            lblAng2.Text = contactUsText.AngSearchText;

            lblCreatePothi.Text = contactUsText.CreatePothi;
            lblCreatePothi1.Text = contactUsText.CreatePothi1;
            lblCreatePothi2.Text = contactUsText.CreatePothi2;
            lblCreatePothi3.Text = contactUsText.CreatePothi3;
            lblCreatePothi4.Text = contactUsText.CreatePothi4;
            lblCreatePothi5.Text = contactUsText.CreatePothi5;

            lblAddShabad.Text = contactUsText.AddShabad;
            lblAddShabad1.Text = contactUsText.AddShabad1;
            lblAddShabad2.Text = contactUsText.AddShabad2;
            lblAddShabad4.Text = contactUsText.AddShabad4;

            lblSimilarShabad.Text = contactUsText.SimilarShabads;
            lblSimilarShabad1.Text = contactUsText.SimilarShabads1;
            lblSimilarShabad2.Text = contactUsText.SimilarShabads2;
            lblSimilarShabad3.Text = contactUsText.SimilarShabads3;

            lblAkv.Text = contactUsText.AsaKiVaarMode;
            lblAkv1.Text = contactUsText.AsaKiVaarMode1;
            lblAkv2.Text = contactUsText.AsaKiVaarMode2;
            lblAkv3.Text = contactUsText.AsaKiVaarMode3;
        }

        protected override void OnAppearing()
        {
            Theme theme = new Theme();
            BindingContext = theme;
        }

        private void btnSend_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("KeertanPothi@gmail.com", "KeertanPothi1699"),
                    EnableSsl = true
                };
                client.Send("KeertanPothi@gmail.com", "nonubitta@gmail.com", txtSubject.Text, txtEmail.Text + ": " + txtBody.Text);
                Util.ShowRoast("Message sent !!!", true);
            }
            catch (Exception ex)
            {
                Util.ShowRoast("Failed to send message");
            }
        }

        private void exp1_Tapped(object sender, EventArgs e)
        {
            scrMain.ScrollToAsync(0, 0, true);
            if (exp1.IsExpanded)
            {
                lbl1.Text = "↑";
                exp2.IsExpanded = false;
                exp3.IsExpanded = false;
                lbl3.Text = "↓";
                lbl2.Text = "↓";
            }
            else
                lbl1.Text = "↓";
        }

        private void exp2_Tapped(object sender, EventArgs e)
        {
            if (exp2.IsExpanded) { 
                lbl2.Text = "↑";
                exp1.IsExpanded = false;
                exp3.IsExpanded = false;
                lbl1.Text = "↓";
                lbl3.Text = "↓";
            }
            else
                lbl2.Text = "↓";
        }

        private void exp3_Tapped(object sender, EventArgs e)
        {
            if (exp3.IsExpanded) { 
                lbl3.Text = "↑";
                exp1.IsExpanded = false;
                exp2.IsExpanded = false;
                lbl1.Text = "↓";
                lbl2.Text = "↓";
            }
            else
                lbl3.Text = "↓";
        }



        private async void TapGestureRecognizer_Punjabi(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://youtu.be/jsny2dOcLjU");
        }

        private async void TapGestureRecognizer_English(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://youtu.be/HVJgWXapYiA");
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync("https://youtu.be/jsny2dOcLjU");
        }

        void exp5_Tapped(System.Object sender, System.EventArgs e)
        {
            if (exp5.IsExpanded)
            {
                lbl5.Text = "↑";
                exp1.IsExpanded = false;
                exp2.IsExpanded = false;
                exp3.IsExpanded = false;
                exp4.IsExpanded = false;
                lbl1.Text = "↓";
                lbl2.Text = "↓";
                lbl3.Text = "↓";
                lbl4.Text = "↓";
            }
            else
                lbl5.Text = "↓";
        }

        void exp4_Tapped(System.Object sender, System.EventArgs e)
        {
            if (exp4.IsExpanded)
            {
                lbl4.Text = "↑";
                exp1.IsExpanded = false;
                exp2.IsExpanded = false;
                exp3.IsExpanded = false;
                exp5.IsExpanded = false;
                lbl1.Text = "↓";
                lbl2.Text = "↓";
                lbl3.Text = "↓";
                lbl5.Text = "↓";
            }
            else
                lbl4.Text = "↓";
        }
    }
}
