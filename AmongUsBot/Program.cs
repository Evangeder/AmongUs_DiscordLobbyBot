using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AmongUsBot
{
    class Program
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        private Events.Events events;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            var token = "paste your token here";

            events = new Events.Events(_client, _commands, _services);

            _client.Log                     += events.Log;
            _client.ReactionAdded           += events.ReactionAdded;
            _client.ReactionRemoved         += events.ReactionRemoved;
            _client.UserVoiceStateUpdated   += events.UserVoiceStateUpdated;

            await _client.SetGameAsync("Among Us", null, ActivityType.Playing);

            await RegisterCommandsAsync();
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(2500);
            foreach (var guild in _client.Guilds)
            {
                bool deleted = false;
                ulong _categoryId = 0;

                foreach (var category in guild.CategoryChannels.ToList())
                    if (string.Compare(category.Name, Config.LobbyCategoryName, true) == 0)
                    {
                        _categoryId = category.Id;
                        break;
                    }

                if (_categoryId > 0 && guild.VoiceChannels.Count > 0)
                    foreach (var vChan in guild.VoiceChannels)
                        if (vChan.CategoryId == _categoryId && vChan.Name != Config.VoiceChannelHub)
                        {
                            deleted = true;
                            await vChan.DeleteAsync();
                        }

                var ranks = guild.Roles.Where(x => x.Name.StartsWith(Config.VoiceRoomPrefix));
                if (ranks.Count() > 0)
                    foreach (var rank in ranks)
                    {
                        deleted = true;
                        await rank.DeleteAsync();
                    }

                if (_categoryId > 0 && deleted)
                    foreach(var chan in guild.TextChannels)
                        if (chan.Name == Config.LobbyCategoryName && chan.CategoryId == _categoryId)
                            await chan.SendMessageAsync(Strings.AutoRestartMessageWhenCrash(guild.Roles.Max().Mention));
            }

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += events.MessageReceived;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }
    }
}
