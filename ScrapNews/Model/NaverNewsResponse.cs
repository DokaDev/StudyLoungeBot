using System.Text.Json.Serialization;

namespace ScrapNews.Model {
    public class NaverNewsResponse {
        public string LastBuildDate { get; set; }
        public int Total { get; set; }
        public int Start { get; set; }
        public int Display { get; set; }

        [JsonPropertyName("items")]
        public List<NaverNewsModel> NaverNews { get; set; } = new();
    }
}
