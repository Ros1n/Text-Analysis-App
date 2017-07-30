using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TextAnalytics2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnButton1(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            await Navigation.PushModalAsync(new TypingPage());
        }

        async void OnButton2(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            await Navigation.PushModalAsync(new RecordPage());
        }

        async void OnButton3(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            //await DisplayAlert("this is not work", "oh shit", "ok");
        }
    }
}
