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
    public class DocumentDetails
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
                public string date { get; set; }
                public List<Comment> comments { get; set; }
                public List<History> history { get; set; }
            }

            public class History
            {
                public string id { get; set; }
                public string title { get; set; }
                public string file { get; set; }
                public string date { get; set; }
            }

            public class Comment
            {
                public string text { get; set; }
                public string type { get; set; }
                public string user { get; set; }
                public string file { get; set; }
                public string date { get; set; }
            }
        }
    }

    public class DocumentDetailsData
    {
        private ObservableCollection<DocumentDetails> _DocDatas = new ObservableCollection<DocumentDetails>();
        public ObservableCollection<DocumentDetails> DocDatas
        {
            get
            {
                return this._DocDatas;
            }
        }
        public async Task GetDocumentDetailsData(string sessionId, string deviceId, string id)
        {
            if ((DocDatas.Count > 0) || (_DocDatas.Count > 0))
            {
                DocDatas.Clear();
                _DocDatas.Clear();
            }

            string post = "sessionId=" + sessionId + "&deviceId=" + deviceId + "&id=" + id;
            Task<string> result = WebRequests.GetWebResponse(post, ((App)(App.Current)).apiUrl + "getDocument");

            string results = await result;
            var jsonParse = JsonConvert.DeserializeObject<DocumentDetails>(results);
            this.DocDatas.Add(jsonParse);
        }
    }
}