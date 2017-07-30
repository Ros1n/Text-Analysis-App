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
using static TextAnalytics2.responseModel.responseObj;

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
                    //send to url1
                    response = await client.PostAsync(url1, httpclient);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var resp = JsonConvert.DeserializeObject<documentsModel>(responseString);
                        foreach (var item in resp.documents)
                        {
                            sentiment_id.Text = item.id;
                            score.Text = item.score.ToString();
                        }
                    }
                    else
                    {
                        await DisplayAlert("title", String.Format("exception with statuscode {0}", response.StatusCode), "OK");
                    }
                    //send to url2
                    /**
                    response = await client.PostAsync(url2, httpclient);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString2 = await response.Content.ReadAsStringAsync();
                        var resp2 = JsonConvert.DeserializeObject<responseObj>(responseString2);
                        foreach (var item in resp2.documentDetail)
                        {
                            textId.Text = item.textId;
                            score.Text = item.score.ToString();
                        }
                        textId.Text = resp2.textId;
                        keyPharse.Text = resp2.KeyPharses.ToString();
                    }
                    else
                    {
                        await DisplayAlert("title", String.Format("exception with statuscode {0}", response.StatusCode), "OK");
                    }
                    //send to url3
                    response = await client.PostAsync(url3, httpclient);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString3 = await response.Content.ReadAsStringAsync();
                        var resp3 = JsonConvert.DeserializeObject<responseObj>(responseString3);
                        List<language> lang = resp3.detectedLanguages;
                        foreach (var item in lang)
                        {
                            languageName.Text = item.name;
                            languageScore.Text = item.score.ToString();
                        }
                    }
                    else
                    {
                        await DisplayAlert("title", String.Format("exception with statuscode {0}", response.StatusCode), "OK");
                    }
                    **/

                }
                    //send response data to easyTable
                    var client2 = new HttpClient();
                    HttpResponseMessage response2;
                    client2.DefaultRequestHeaders.Add("ZUMO-API-VERSION", "2.0.0");
                    string geturl = "https://textanalytics2.azurewebsites.net/tables/textAnalytics2result";
                    var content2 = JsonConvert.SerializeObject(new { sentiment_id = sentiment_id.Text, score = score.Text });
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