using System.Runtime.CompilerServices;
using HtmlAgilityPack;

namespace LunchMenu {
    public class GetImage {
        public async Task<string> Request() {
            string imgUrl = string.Empty;
            using(HttpClient client = new()) {
                string url = "https://www.bobful.com/bbs/cook_list.php?bo_table=cook&cook_id=24";
                try {
                    string htmlSource = await client.GetStringAsync(url);
                    //Console.WriteLine(htmlSource);

                    imgUrl = await GetImageUrl(htmlSource);
                    return imgUrl;
                } catch(Exception e) {
                    Console.WriteLine(e);
                }
            }
            //Console.WriteLine($"{imgUrl}");
            return string.Empty;            
        }
        public async Task<string> GetImageUrl(string src) {
            string html = src;

            HtmlDocument doc = new();
            doc.LoadHtml(html);

            HtmlNode aTag = doc.DocumentNode.SelectSingleNode("//a[@class='view_image']");
            string hrefValue = aTag.GetAttributeValue("href", string.Empty);

            //Console.WriteLine($"{hrefValue}");
            return hrefValue;
        }
    }
}
