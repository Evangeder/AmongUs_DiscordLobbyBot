using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace AmongUsBot.Events
{
    partial class Events
    {
        internal async Task MessageReceived(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix(Config.CommandPrefix.ToString(), ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
        }
    }
}
