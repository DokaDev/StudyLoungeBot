using Discord;
using Discord.Commands;
using Discord.Interactions;

namespace BotCore {
    public class LoggingService {
        public CoreModule CoreModule { get; private set; }
        public LoggingService(CoreModule coreModule) {
            coreModule.Client.Log += LogAsync;
            coreModule.CommandService.Log += LogAsync;
            //CoreModule.InteractionService.Log += LogAsync;

            coreModule.InteractionService.SlashCommandExecuted += SlashCommandExecutedAsync;
        }

        private Task LogAsync(LogMessage msg) {
            if(msg.Exception is CommandException cmdException) {
                Console.WriteLine($"[Command/{msg.Severity}] {cmdException.Command.Aliases.First()}"
                    + $" failed to execute in {cmdException.Context.Channel}.");
                Console.WriteLine(cmdException);
            } else
                Console.WriteLine($"[General/{msg.Severity}] {msg}");

            return Task.CompletedTask;
        }

        private Task SlashCommandExecutedAsync(SlashCommandInfo cmdInfo, IInteractionContext context, Discord.Interactions.IResult result) {
            if(!result.IsSuccess)
                Console.WriteLine("");
            else
                Console.WriteLine("");

            return Task.CompletedTask;
        }
    }
}
