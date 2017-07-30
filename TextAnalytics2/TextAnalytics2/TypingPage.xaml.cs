using Foundation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TextAnalytics2.requestModel;
using TextAnalytics2.responseModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TextAnalytics2.documents;

namespace TextAnalytics2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TypingPage : ContentPage
    {
        public TypingPage()
        {
            InitializeComponent();
        }

        async void OnSendButton(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            await DisplayAlert("title", "message", "OK");
        }

        async Task MakeAnalyticsRequest()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "c2bbf76f4130457ca8915b44390d8776");
            string url1 = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";
            string url2 = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/keyPhrases";
            string url3 = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/languages";

            HttpResponseMessage response;
            var text = (string) sentence.Text;
            if (text != null && text.Length > 0)
            {
                var id_tmp = id.Text;
                var lan = language.Text ?? "";

                var req = new requestObj();
                req.text = text;
                req.id = id_tmp;
                req.language = lan;

                List<requestObj> ls = new List<requestObj>() { req};
                var content = JsonConvert.SerializeObject(new { documents=ls});
                using (var httpclient = new StringContent(content, Encoding.UTF8, "application/json"))
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    response = await client.PostAsync(url1, httpclient);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var resp = JsonConvert.DeserializeObject<documentsModel>(responseString);
                        List<details> document = resp.documents;
                        foreach (var item in document)
                        {
                            tag1.Text = item.id;
                            tag2.Text = item.score;
                        }
                    }
                    else
                    {
                        await DisplayAlert("title", String.Format("exception with statuscode {0}", response.StatusCode), "OK");
                    }
                }
                    var client2 = new HttpClient();
                    HttpResponseMessage response2;
                    client2.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                    string geturl = "https://textanalytics2.azurewebsites.net/tables/textAnalytics2result";
                    var content2 = JsonConvert.SerializeObject(new { sentiment_id = tag1.Text, score = tag2.Text });
                    using (var httpclient2 = new StringContent(content2, Encoding.UTF8, "application/json"))
                    {
                        client2.DefaultRequestHeaders.Add("Accept", "application/json");
                        response2 = await client2.PostAsync(geturl, httpclient2);
                        if (!response2.IsSuccessStatusCode)
                        {
                            await DisplayAlert("title", "cannot send data to easytable", "OK");
                        }
                        else
                        {
                        await DisplayAlert("title", "successfully send data to easytable", "OK");
                        }
                    }


                }





        }
    }
}