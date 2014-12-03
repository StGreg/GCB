using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GCB
{

    public class InvestmentDetails
    {
        public string status { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public Data2 data { get; set; }

            public class Data2
            {
                public string title { get; set; }
                public string place { get; set; }
                public DateTime date_from { get; set; }
                public DateTime date_to { get; set; }
                public DateTime date_offer { get; set; }
                public string description { get; set; }
                public string id { get; set; }
                public List<Branch> branches { get; set; }
                public List<AssignedBranch> assignedBranches { get; set; }

                public class AssignedBranch
                {
                    public string id { get; set; }
                    public string title { get; set; }
                }

                public class Branch
                {
                    public string id { get; set; }
                    public string name { get; set; }
                    public Offer offer { get; set; }
                    public int assigned { get; set; }
                }

                public class Offer
                {
                    public string price { get; set; }
                    public DateTime date { get; set; }
                }
            }
        }
    }

    public class InvestmentDetailsData
    {
        private ObservableCollection<InvestmentDetails> _InvDetDatas = new ObservableCollection<InvestmentDetails>();
        public ObservableCollection<InvestmentDetails> InvDetDatas
        {
            get
            {
                return this._InvDetDatas;
            }
        }
        public async Task GetInvestemntDetailsData(string sessionId, string deviceId, string id)
        {

            if ((InvDetDatas.Count > 0) || (_InvDetDatas.Count > 0))
            {
                InvDetDatas.Clear();
                _InvDetDatas.Clear();
            }

            string post = "sessionId=" + sessionId + "&deviceId=" + deviceId + "&id=" + id;
            Task<string> result = WebRequests.GetWebResponse(post, ((App)(App.Current)).apiUrl + "getInvestment");

            string results = await result;
            StringBuilder a = new StringBuilder(results);
            a.Replace("\"offer\":[]", "\"offer\":{}");
            results = a.ToString();
            var jsonParse = JsonConvert.DeserializeObject<InvestmentDetails>(results);
            this.InvDetDatas.Add(jsonParse);
        }
    }
}
