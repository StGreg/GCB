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
    public class InvestmentResponse
    {
        public string status { get; set; }
        public Data data { get; set; }

        public class Data
        {
            public List<List> list { get; set; }
        }

        public class List
        {
            public string title { get; set; }
            public string place { get; set; }
            public DateTime date_from { get; set; }
            public DateTime date_to { get; set; }
            public DateTime date_offer { get; set; }
            public string id { get; set; }
            public Offer offer { get; set; }
            public List<AssignedBranches> assignedBranches { get; set; }

            public class AssignedBranches
            {
                public string id { get; set; }
                public string title { get; set; }
            }

            public class Offer
            {
                public string branchId { get; set; }
                public string branchName { get; set; }
                public string price { get; set; }
                public DateTime date { get; set; }

            }
        }
    }

    public class InvestmentListData
    {
        private ObservableCollection<InvestmentResponse> _InvDatas = new ObservableCollection<InvestmentResponse>();
        public ObservableCollection<InvestmentResponse> InvDatas
        {
            get
            {
                return this._InvDatas;
            }
        }

        public async Task GetInvestemntListData(string sessionId, string deviceId, string stat)
        {
            if ((InvDatas.Count > 0) || (_InvDatas.Count > 0))
            {
                InvDatas.Clear();
                _InvDatas.Clear();
            }

            string post = "sessionId=" + sessionId + "&deviceId=" + deviceId + "&stat=" + stat;
            Task<string> result = WebRequests.GetWebResponse(post, ((App)(App.Current)).apiUrl + "getInvestmentList");

            string results = await result;
            StringBuilder a = new StringBuilder(results);
            a.Replace("\"offer\":[]", "\"offer\":{}");
            results = a.ToString();
            var jsonParse = JsonConvert.DeserializeObject<InvestmentResponse>(results);
            this.InvDatas.Add(jsonParse);
        }
    }
}

