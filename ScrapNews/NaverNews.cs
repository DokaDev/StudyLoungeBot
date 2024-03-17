using ScrapNews.Model;
using System.Net;
using System.Text.Json;

namespace ScrapNews {
    public class NaverNews {
        private readonly string? APIID;
        private readonly string? APISecretKey;

        private string url = "https://openapi.naver.com/v1/search/news.json";

        public NaverNews() {
            APIID = Environment.GetEnvironmentVariable("NaverApiId");
            APISecretKey = Environment.GetEnvironmentVariable("NaverApiSecretKey");
        }

        /// <summary>
        /// Requests the specified query.
        /// </summary>
        /// <param name="query">Keyword for search</param>
        /// <param name="display">Number of results</param>
        public async Task<NaverNewsResponse> RequestQuery(string query, string display = "10") {
            // debug
            //Console.WriteLine("NaverNews.Request");

            url = $"{url}?query={query}&display={display}&sort=sim";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", APIID);
            request.Headers.Add("X-Naver-Client-Secret", APISecretKey);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();

            if(status != "OK")
                throw new HttpRequestException($"Error in request to Naver API. {status}");

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new(dataStream);
            string responseFromServer = await reader.ReadToEndAsync();

            // debug
            Console.WriteLine(responseFromServer);

            // Decode Json Text
            //responseFromServer = WebUtility.HtmlDecode(responseFromServer);
            //responseFromServer = responseFromServer.Replace("&amp;", "&");
            //responseFromServer = responseFromServer.Replace("&lt;", "<");
            //responseFromServer = responseFromServer.Replace("&gt;", ">");
            //responseFromServer = responseFromServer.Replace("&nbsp;", " ");
            //responseFromServer = responseFromServer.Replace("&quot;", "'");
            //responseFromServer = responseFromServer.Replace("&#39;", "'");
            //responseFromServer = responseFromServer.Replace("<b>", "");
            //responseFromServer = responseFromServer.Replace("</b>", "");

            // debug
            //Console.WriteLine("Decoded");
            //Console.WriteLine(responseFromServer);

            // Json Text -> Object(NaverNewsResponse)
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            NaverNewsResponse newsResponse = JsonSerializer.Deserialize<NaverNewsResponse>(responseFromServer, options);

            //NaverNewsResponse naverNewsResponse = JsonSerializer.Deserialize<NaverNewsResponse>(responseFromServer);
            return newsResponse;
        }
    }
}
