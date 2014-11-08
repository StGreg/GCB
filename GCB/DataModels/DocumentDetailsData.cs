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
            string post = "sessionId=" + sessionId + "&deviceId=" + deviceId + "&id=" + id;
            Task<string> result = GetWebResponse(post, "http://www.gcb.osostudio.pl/api/action/getDocument");

            string results = await result;
            var jsonParse = JsonConvert.DeserializeObject<DocumentDetails>(results);
            this.DocDatas.Add(jsonParse);
        }

        private async Task<string> GetWebResponse(string postData, string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            string result;
            webRequest.Proxy = null;
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";

            using (Stream postStream = await webRequest.GetRequestStreamAsync())
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                await postStream.WriteAsync(byteArray, 0, byteArray.Length);
                await postStream.FlushAsync();
            }

            try
            {
                string Response;
                var response = (HttpWebResponse)await webRequest.GetResponseAsync();
                using (Stream streamResponse = response.GetResponseStream())
                using (StreamReader streamReader = new StreamReader(streamResponse))
                {
                    Response = await streamReader.ReadToEndAsync();
                }
                if (Response != null)
                {
                    result = parse(Response);
                }
                else
                {
                    result = "Błąd odpowiedzi serwera!";
                }
            }
            catch (WebException)
            {
                result = "Błąd połączenia";
            }

            return result;
        }

        private string parse(string input)
        {
            StringBuilder a = new StringBuilder(input);
            a.Replace("\\u0105", "ą");
            a.Replace("\\u0107", "ć");
            a.Replace("\\u0119", "ę");
            a.Replace("\\u0142", "ł");
            a.Replace("\\u0144", "ń");
            a.Replace("\\u00f3", "ó");
            a.Replace("\\u015b", "ś");
            a.Replace("\\u017a", "ź");
            a.Replace("\\u017c", "ż");
            a.Replace("\\u0104", "Ą");
            a.Replace("\\u0106", "Ć");
            a.Replace("\\u0118", "Ę");
            a.Replace("\\u0141", "Ł");
            a.Replace("\\u0143", "Ń");
            a.Replace("\\u00d3", "Ó");
            a.Replace("\\u015a", "Ś");
            a.Replace("\\u0179", "Ź");
            a.Replace("\\u017b", "Ż");
            return a.ToString();
        }
    }
}