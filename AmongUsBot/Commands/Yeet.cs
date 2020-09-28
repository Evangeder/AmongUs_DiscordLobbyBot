using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace AmongUsBot.Commands
{
	public class YeetModule : ModuleBase<SocketCommandContext>
	{
		[Command("yeet")]
		public Task SayAsync(string player)
		{
			bool impostor = new Random().Next(0, 10) < 5;
			int left = new Random().Next(0, 2);

			if (Context.Message.MentionedUsers.Count > 0)
				return ReplyAsync(Strings.Yeet(player, impostor, left));

			return ReplyAsync(Strings.YeetFail(Context.Message.Author.Mention));
		}
	}
}
