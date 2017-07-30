using Android.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TextAnalytics2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecordPage : ContentPage
    {
        public RecordPage()
        {
            InitializeComponent();
        }

        async Task listRecordAsync()
        {
            var client = new HttpClient();
            HttpResponseMessage response;
            client.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            string geturl = "https://textanalytics2.azurewebsites.net/tables/textAnalytics2result";
            
            response = await client.GetAsync(geturl);
            if (!response.IsSuccessStatusCode)
            {
                await DisplayAlert("title", "cannot send GET request", "OK");
            }
            var responseString = await response.Content.ReadAsStringAsync();

            ObservableCollection<easyResponse> ls = new ObservableCollection<easyResponse>();
            var resp = JsonConvert.DeserializeObject<List<easyResponse>>(responseString);
/**
            ListView lv = new ListView();
            lv.ItemsSource = ls;
            ls.Add( new easyResponse(){ score = "6" } );
**/
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            this.Content = layout;

            foreach (var item in resp)
            {
                var label = new Label { Text = "text_id: " + item.textId + "sentiment_id: " + item.sentiment_id+ "score: " + item.score };
                layout.Children.Add(label);
            }
        }
    }
    
    public class easyResponse
    {
        public string textId { get; set; }
        public string sentiment_id { get; set; }
        public string score { get; set; }
    }
}