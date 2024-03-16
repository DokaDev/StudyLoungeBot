using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BotCore {
    public class CoreModule {
        public DiscordSocketConfig Config { get; private set; }

        public DiscordSocketClient? Client { get; private set; }
        public CommandService? CommandService { get; private set; }
        public LoggingService? LoggingService { get; private set; }

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
            LoggingService = new(Client, CommandService);            
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
            Client.MessageUpdated += MessageUpdated;
            Client.MessageReceived += MessageReceived;
            Client.Ready += () => {
                Console.WriteLine("Bot ready");
                return Task.CompletedTask;
            };
        }

        /// <summary>
        /// Handles the message updated event.
        /// </summary>
        /// <param name="before">cacheable message, so it may not exist</param>
        /// <param name="after">updated message</param>
        /// <param name="channel">the message was updated in</param>
        /// <returns></returns>
        private async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel) {
            // If the message was not in the cache, downloading it will result in getting a copy of the message.
            IMessage msg = await before.GetOrDownloadAsync();
            Console.WriteLine($"{msg} -> {after}");
        }

        private async Task MessageReceived(SocketMessage msg) {
            if(msg.Author.Id == Client.CurrentUser.Id)
                return;

            // Log the message to the console
            Console.WriteLine($"{msg.Author.Username} -> {msg.Content}");
            // todo. handle commands
        }
    }
}
