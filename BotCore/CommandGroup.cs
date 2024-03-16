using Discord.Interactions;

namespace BotCore {
    public class CommandGroup : InteractionModuleBase<SocketInteractionContext> {
        [SlashCommand("ping", "Replies with pong")]
        public async Task Ping() {
            await RespondAsync("Pong!");
        }
    }
}
