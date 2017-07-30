using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalytics2.responseModel
{
    class responseObj
    {
        public string textId { get; set; }
        public double score { get; set; }
        public string KeyPharses { get; set; }
        public List<language> detectedLanguages { get; set; }

        public class language
        {
            public string name { get; set; }
            public string iso6391Name { get; set; }
            public double score { get; set; }
        }

    }
}
