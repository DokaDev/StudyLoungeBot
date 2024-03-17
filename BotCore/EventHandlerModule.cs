using Discord.WebSocket;
using Discord;
using Discord.Interactions;
using System.Numerics;

namespace BotCore
{
    public class EventHandlerModule {
        public CoreModule CoreModule { get; private set; }
        public EventHandlerModule(CoreModule coreModule) {
            CoreModule = coreModule;
        }

        public async Task ReadyAsync() {
            if(CoreModule.Client is null)
                return;

            Console.WriteLine("Bot ready");
            if(CoreModule.Client.ShardId == 0) {
                // Only one shard is ready, so we can do some one-time setup
                await CoreModule.InteractionService.AddModuleAsync<CommandGroup>(null);

                // Register the commands
                // Based on GuildId for Test Server
                ulong guildId = 1218399522393427998;
                //try {
                //    guildId = ulong.Parse(Environment.GetEnvironmentVariable("DiscordDevTestServerGuildId") ?? throw new InvalidOperationException("Not assigned variable to DiscordDevTestServerGuildId"));
                //} catch(Exception ex) {
                //    Console.WriteLine(ex);
                //    return;
                //}

                await CoreModule.InteractionService.RegisterCommandsToGuildAsync(guildId);

                // todo. Based on Global
                await CoreModule.InteractionService.RegisterCommandsGloballyAsync();
            }
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
            if(CoreModule.Client is null)
                return;

            if(msg.Author.Id == CoreModule.Client.CurrentUser.Id)
                return;

            // Log the message to the console
            Console.WriteLine($"{msg.Author.Username} -> {msg.Content}");
            // todo. handle commands
        }
    }
}
