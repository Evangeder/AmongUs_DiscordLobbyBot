using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace AmongUsBot.Commands
{
	public class HelpModule : ModuleBase<SocketCommandContext>
	{
		[Command("help")]
		public Task SayAsync() 
			=> Context.Message.Author.SendMessageAsync(Strings.Help(Context.Message.Author.Mention));
	}
}
