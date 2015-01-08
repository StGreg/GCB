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
    public class BranchDetails
    {
        public string status { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public Data2 data { get; set; }

            public class Data2
            {
                public string id { get; set; }
                public string title { get; set; }
                public Offer offer { get; set; }
                public List<Document> documents { get; set; }
                public string agreement { get; set; }
                public string isAgreementSigned { get; set; }
                public string isProtocolSigned { get; set; }
                public string adminAgreementSignDate { get; set; }
                public string userAgreementSignDate { get; set; }
                public string adminProtocolSignDate { get; set; }
                public string userProtocolSignDate { get; set; }
                public List<object> stages { get; set; }

                public class Document
                {
                    public string id { get; set; }
                    public string title { get; set; }
                    public string file { get; set; }
                    public int commentCount { get; set; }
                    public DateTime date { get; set; }
                }

                public class Offer
                {
                    public string price { get; set; }
                    public DateTime date { get; set; }
                }
            }
        }
    }

    public class BranchDetailsData
    {
        private ObservableCollection<BranchDetails> _BranchDatas = new ObservableCollection<BranchDetails>();
        public ObservableCollection<BranchDetails> BranchDatas
        {
            get
            {
                return this._BranchDatas;
            }
        }
        public async Task GetBranchDetailsData(string sessionId, string deviceId, string branch_id, string investment_id)
        {
            if ((BranchDatas.Count > 0) || (_BranchDatas.Count > 0))
            {
                BranchDatas.Clear();
                _BranchDatas.Clear();
            }

            string post = "sessionId=" + sessionId + "&deviceId=" + deviceId + "&branch_id=" + branch_id + "&investment_id=" + investment_id;
            Task<string> result = WebRequests.GetWebResponse(post, ((App)(App.Current)).apiUrl + "getBranch");

            string results = await result;
            StringBuilder a = new StringBuilder(results);
            a.Replace("\"offer\":[]", "\"offer\":{}");
            results = a.ToString();
            var jsonParse = JsonConvert.DeserializeObject<BranchDetails>(results);
            this.BranchDatas.Add(jsonParse);
        }
    }
}
