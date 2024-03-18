using Discord;
using Discord.Interactions;
using ScrapNews;
using ScrapNews.Model;
using System.Net;

namespace BotCore {
    public class CommandGroup : InteractionModuleBase<SocketInteractionContext> {
        [SlashCommand("ping", "Replies with pong")]
        public async Task Ping() {
            await RespondAsync("Pong!");
        }

        // 
        [SlashCommand("task", "새 태스크 생성")]
        public async Task SetTask() {
            //var embed = new EmbedBuilder()
            //    .WithTitle($"에 대한 뉴스 검색 결과")
            //    .WithColor(Color.Orange)      // Color.Blue
            //    .WithCurrentTimestamp();    // Current Time
            
            await Task.Delay(-1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="display"></param>
        [SlashCommand("news", "Get news from Naver")]
        public async Task GetNews(
            [Summary("keywords", "검색어")] string keywords,
            [Summary("display", "검색 결과 수")] int display = 10
            ) {
            // test
            NaverNews naverNews = new();
            NaverNewsResponse response = new(); // blank response
            try {
                response = await naverNews.RequestQuery(keywords, display.ToString());
            } catch(Exception e) {
                Console.WriteLine(e);
                await RespondAsync("Error");
            }

            //await RespondAsync(response.Length.ToString());
            string formatted = "";
            foreach(NaverNewsModel news in response.NaverNews) {
                formatted += $"[{news.Title}]({news.Link})\n";
            }

            //await RespondAsync(formatted);
            // Create Embed
            var embed = new EmbedBuilder()
                .WithTitle($"'{keywords}'에 대한 뉴스 검색 결과")
                .WithColor(Color.Orange)      // Color.Blue
                .WithCurrentTimestamp();    // Current Time

            // add fields to embed for each news
            foreach(NaverNewsModel news in response.NaverNews) {
                embed.AddField(field => {
                    field.Name = news.Title.Length > 256 ? WebUtility.HtmlDecode(news.Title.Substring(0, 253)) + "..." : news.Title;
                    field.Value = news.Link;
                    field.IsInline = false;
                });

                if(embed.Fields.Count == 25) // limit 25 fields
                    break;
            }

            if(embed.Fields.Count > 0)
                await RespondAsync(embed: embed.Build());
            else
                await RespondAsync("no result found.");
        }


    }
}
