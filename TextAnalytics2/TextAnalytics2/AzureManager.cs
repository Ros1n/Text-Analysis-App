using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAnalytics2.outputTable;

namespace TextAnalytics2
{
    public class AzureManager
    {
        public static AzureManager instance;
        public MobileServiceClient client;
        private IMobileServiceTable<textAnalytics2Table> textAnalytics2Table;

        public AzureManager()
        {
            this.client = new MobileServiceClient("http://purplehat.azurewebsites.net");
            this.textAnalytics2Table = this.client.GetTable<textAnalytics2Table>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }
        /**
        public async Task<List<textAnalytics2Table>> GetInformation()
        {
            return await this.textAnalytics2Table.ToListAsync();
            //invoke azuremanager, get all info in table, convert to list, store in the model we create
        }

        public async Task PostInformation(textAnalytics2Table table)
        {
            await this.textAnalytics2Table.InsertAsync(table);
        }
    **/
    }
}
