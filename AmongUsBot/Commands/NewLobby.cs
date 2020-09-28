using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace AmongUsBot.Commands
{
	public class NewLobbyModule : ModuleBase<SocketCommandContext>
	{
        [Command("nowa")] public Task SayAsync() => ReplyAsync(Strings.InfoNewRoom);
		[Command("nowa")] public Task SayAsync(string isPrivate) => ReplyAsync(Strings.InfoNewRoom);
		[Command("nowa")] public Task SayAsync(string isPrivate, string pcount) => SayAsync(isPrivate, pcount, "1");
		[Command("nowa")] public Task SayAsync(int pcount, int icount) => SayAsync("public", pcount.ToString(), icount.ToString());
		[Command("nowa")] public Task SayAsync(string isPrivate, string pcount, string icount)
		{
			var mention = Context.Message.Author.Mention;
			var user	= Context.Guild.GetUser(Context.Message.Author.Id);

			if (user.VoiceChannel == null)
				return ReplyAsync(Strings.ErrorMustBeInVoiceLobby(mention));

			if (!int.TryParse(pcount, out int playerCount))		return ReplyAsync(Strings.ErrorPlayerNaN);
			if (!int.TryParse(icount, out int impostorCount))	return ReplyAsync(Strings.ErrorImpostorNaN);

			playerCount		= Mathf.Clamp(playerCount, 5, 10);
			impostorCount	= Mathf.Clamp(impostorCount, 1, 3);

			ulong categoryId = 0;
			foreach (var category in Context.Guild.CategoryChannels.ToList())
				if (string.Compare(category.Name, Config.LobbyCategoryName, true) == 0)
				{
					categoryId = category.Id;
					break;
				}

			if (Config.PrivateKeywords.Contains(isPrivate.ToLower()))
			{
				var vRankRawData = Context.Guild.CreateRoleAsync(Config.VoiceRoomName(Rooms.RoomList.Count + 1), new GuildPermissions(viewChannel: false), null, false, null);
				var message = ReplyAsync(Strings.PrivateRoomCreated(mention));
				var perms = new OverwritePermissions(viewChannel: PermValue.Allow);
				var permsEveryone = new OverwritePermissions(viewChannel: PermValue.Deny);
				var vChanRawData = Context.Guild.CreateVoiceChannelAsync(Config.VoiceRoomName(Rooms.RoomList.Count + 1));
				vChanRawData.Result.ModifyAsync(x => {
					x.UserLimit = playerCount;
					x.CategoryId = categoryId;
				});

				var everyone = Context.Guild.Roles.Where(x => x.Name.Contains("everyone")).First();

				vChanRawData.Result.AddPermissionOverwriteAsync(everyone, permsEveryone);
				vChanRawData.Result.AddPermissionOverwriteAsync(vRankRawData.Result, perms);

				var vChan = new Optional<IVoiceChannel>(vChanRawData.Result);
				user.ModifyAsync(x => x.Channel = vChan);
				user.AddRoleAsync(vRankRawData.Result);

				Rooms.Register(Context.Message.Author, message.Result, playerCount, vChan, true, vRankRawData.Result);

				return Task.CompletedTask;
			}
			else
			{
				var message = ReplyAsync(Strings.NewRoomAnnouncement(mention, playerCount, Context.Message.Author.Username, impostorCount));

				var vChanRawData = Context.Guild.CreateVoiceChannelAsync(Config.VoiceRoomName(Rooms.RoomList.Count + 1));
				vChanRawData.Result.ModifyAsync(x => {
					x.UserLimit = playerCount;
					x.CategoryId = categoryId;
				});

				var vChan = new Optional<IVoiceChannel>(vChanRawData.Result);

				user.ModifyAsync(x => x.Channel = vChan);
				Rooms.Register(Context.Message.Author, message.Result, playerCount, vChan);

				message.Result.AddReactionAsync(Emotes.RedAlive);

				return Task.CompletedTask;
			}
		}
	}
}
