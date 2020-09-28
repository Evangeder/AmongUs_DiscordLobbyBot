using Discord;
using System;
using System.Threading.Tasks;

namespace AmongUsBot.Events
{
    partial class Events
    {
        internal Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
