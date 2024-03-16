using Discord;
using Discord.WebSocket;

namespace BotCore {
    public class CoreModule { 
        public DiscordSocketClient? Client { get; private set; }
        public CoreModule() {
            Client = new DiscordSocketClient();
        }

        public async Task RunAsync() {
            if(Client is null)
                throw new InvalidOperationException("Client is null");

            DiscordSocketConfig config = new() { MessageCacheSize = 100 };

            // Token should be stored in a secure location
            await Client.LoginAsync(TokenType.Bot, "token");
            await Client.StartAsync();

            // Handle events
            Client.MessageUpdated += MessageUpdated;
            Client.Ready += () => {
                Console.WriteLine("Bot ready");
                return Task.CompletedTask;
            };
        }

        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel) {
            // If the message was not in the cache, downloading it will result in getting a copy of the message.
            IMessage msg = await before.GetOrDownloadAsync();
            Console.WriteLine($"{msg} -> {after}");
        }
    }
}
