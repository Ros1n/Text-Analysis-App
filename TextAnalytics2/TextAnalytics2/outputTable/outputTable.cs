using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace TextAnalytics2.outputTable
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    class textAnalytics2Table
    {
        [JsonProperty(PropertyName = "score")]
        public string score;
        [JsonProperty(PropertyName = "keyPharse")]
        public string keyPharse;
        [JsonProperty(PropertyName = "error")]
        public string error;
        [JsonProperty(PropertyName = "language")]
        public string language;
    }
}
