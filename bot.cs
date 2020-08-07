using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using Voice_Commands.Commands;

namespace Voice_Commands
{
    public class Bot
    {
        public static String streamURL;
        public DiscordUser user { get; }
        public DiscordClient client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public DiscordActivity Activity { get; private set; }
        public string StreamUrl { get; private set; }
        public static String guilds { get; }

        public async Task RunAsync()
        {
            Console.Title = "EclipticBot";
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configjson = JsonConvert.DeserializeObject<ConfigJason>(json);

            var config = new DiscordConfiguration
            {
                Token = configjson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true,


            };
            client = new DiscordClient(config);
            client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromSeconds(120)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configjson.Prefix },
                EnableDms = true,
                EnableMentionPrefix = true,
                DmHelp = true,

            };
            client.UserUpdated += async e =>
            {
            };

            client.ClientErrored += async e =>
            {
                await e.Client.ReconnectAsync();
                    Console.Beep();
            };
            client.Ready += async e =>
            {
                Console.Beep(); Console.Beep();
            };
            client.Heartbeated += async e =>
            {
            };
            client.Resumed += async e =>
            {
                Console.WriteLine("EclipticBot is Back Online...");
            };
            client.UserUpdated += async e =>
            {
                Console.WriteLine("A user account has been updated...");
            };
            client.SocketClosed += async e =>
            {
                Console.WriteLine("Connection Terminated....");
                await e.Client.ReconnectAsync();
            };
            client.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("eli gay"))
                {
                    await e.Channel.SendMessageAsync("https://discord.gg/zUdRyxV").ConfigureAwait(false);
                }
                if (e.Message.Content.ToLower().Contains("hacking"))
                {
                    await e.Channel.SendMessageAsync("no they just have a gamer chair").ConfigureAwait(false);
                }
                if (e.Message.Content.ToLower().Contains("hacks"))
                {
                    await e.Channel.SendMessageAsync("no they just have a gamer chair").ConfigureAwait(false);
                }
                if (e.Message.Content.ToLower().Contains("hacker"))
                {
                    await e.Channel.SendMessageAsync("no they just have a gamer chair").ConfigureAwait(false);
                }
                if (e.Message.Content.ToLower().StartsWith("i-"))
                {
                    await e.Channel.SendMessageAsync("i what?\nuse your words\nyou got this i believe in you").ConfigureAwait(false);
                }
                if (e.Message.Author.Mention == UserCommands.mention)
                {
                    if(UserCommands.mention == "<@!560594792774500379>")
                    {
                        return;
                    }
                    if (UserCommands.mention == "<@!703353151771902012>")
                    {
                        return;
                    }
                    else
                    {
                        if (e.Guild.Name == UserCommands.guilds)
                        {
                            await e.Message.DeleteAsync();
                        }
                        else
                        {
                            return;
                        };
                    };
                }
            };
            client.GuildMemberAdded += async e =>
            {
            };
            client.GuildCreated += async e =>
            {
                var guilds = e.Guild.GetDefaultChannel();
                var total = e.Guild.Roles.Count;
                await guilds
                .SendMessageAsync($"Hey {e.Guild.Owner.Mention} thanks for having me here!\nPlease do e.help to find out all the commands and if you need any help contact my owner <@560594792774500379>")
                .ConfigureAwait(false); //sends welcome message to default channel

                Console.WriteLine($"{e.Guild.Name} has a total of {total} roles\nalso it has {e.Guild.MemberCount} ammount of people");


            };
            client.MessageDeleted += async e =>
            {
            };
            Commands = client.UseCommandsNext(commandsConfig);
            Commands.CommandErrored += async e =>
            {
                e.Context.Channel.SendMessageAsync(e.Exception.Message).ConfigureAwait(false);
            };
            Commands.CommandExecuted += async e =>
            {
            };
            Commands.RegisterCommands<UserCommands>();
            await client.ConnectAsync();
            await Task.Delay(-1);
        }
        private Task OnClientReady(ReadyEventArgs e)
        {
            e.Client.UpdateStatusAsync();
            return Task.CompletedTask;
        }


    }
}
