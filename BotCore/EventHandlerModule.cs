using Discord.WebSocket;
using Discord;

namespace BotCore {
    public class EventHandlerModule {
        public DiscordSocketClient? Client { get; private set; }
        public EventHandlerModule(DiscordSocketClient? client) {
            Client = client;
        }
        /// <summary>
        /// Handles the message updated event.
        /// </summary>
        /// <param name="before">cacheable message, so it may not exist</param>
        /// <param name="after">updated message</param>
        /// <param name="channel">the message was updated in</param>
        public async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel) {
            // If the message was not in the cache, downloading it will result in getting a copy of the message.
            IMessage msg = await before.GetOrDownloadAsync();
            Console.WriteLine($"{msg} -> {after}");
        }

        /// <summary>
        /// Handles the message received event
        /// </summary>
        /// <param name="msg">the message received</param>
        public async Task MessageReceived(SocketMessage msg) {
            if(msg.Author.Id == Client.CurrentUser.Id)
                return;

            // Log the message to the console
            Console.WriteLine($"{msg.Author.Username} -> {msg.Content}");
            // todo. handle commands
        }
    }
}
