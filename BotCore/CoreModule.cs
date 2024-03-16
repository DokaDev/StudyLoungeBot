using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;

namespace BotCore {
    public class CoreModule {
        public DiscordSocketConfig Config { get; private set; }

        public DiscordSocketClient? Client { get; private set; }
        public CommandService? CommandService { get; private set; }
        public LoggingService? LoggingService { get; private set; }

        public InteractionService InteractionService { get; private set; }

        public EventHandlerModule? EventHandlerModule { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreModule"/> class.
        /// </summary>
        public CoreModule() {
            Config = new() {
                MessageCacheSize = 100,
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent,
                AlwaysDownloadUsers = true
            };

            Client = new DiscordSocketClient(Config);
            
            CommandService = new CommandService();
            InteractionService = new(Client);
            EventHandlerModule = new(this);
            LoggingService = new(this);                                
        }

        /// <summary>
        /// Runs the bot asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">Client, CommandService, or LoggingService is null.</exception>
        public async Task RunAsync() {
            if(Client is null)
                throw new InvalidOperationException("Client is null");
            if(CommandService is null)
                throw new InvalidOperationException("CommandService is null");
            if(LoggingService is null)
                throw new InvalidOperationException("LoggingService is null");

            // Token should be stored in a secure location
            await Client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DiscordToken"));
            await Client.StartAsync();

            // Handle events
            Client.MessageUpdated += EventHandlerModule.MessageUpdated;
            Client.MessageReceived += EventHandlerModule.MessageReceived;
            Client.Ready += EventHandlerModule.ReadyAsync;
            Client.InteractionCreated += async (interaction) => {
                var ctx = new SocketInteractionContext(Client, interaction);
                await InteractionService.ExecuteCommandAsync(ctx, null);
            };
        }
    }
}
