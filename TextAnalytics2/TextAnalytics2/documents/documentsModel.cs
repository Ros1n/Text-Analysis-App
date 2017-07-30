using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalytics2.documents
{
    class documentsModel
    {
        public List<details> documents { get; set; }
        public List<string> errors { get; set; }
    }

    public class details
    {
        public string id;
        public string score;
    }

}
