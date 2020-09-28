using Discord.Commands;
using Discord.WebSocket;
using System;

namespace AmongUsBot.Events
{
    partial class Events
    {
        internal DiscordSocketClient _client { get; private set; }
        private CommandService _commands;
        private IServiceProvider _services;

        internal Events(DiscordSocketClient client, CommandService commands, IServiceProvider services)
        {
            _client = client;
            _commands = commands;
            _services = services;
        }
    }
}
