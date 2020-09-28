using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace AmongUsBot.Commands
{
	public class KickModule : ModuleBase<SocketCommandContext>
	{
		[Command("kick")]
		public Task SayAsync(string player)
	{
			string mention = Context.Message.Author.Mention;

			if (Rooms.RoomList.Count <= 0)
				return ReplyAsync(Strings.ErrorRoomNotCreatedYet(mention));

			var roomList = Rooms.RoomList.Where(x => x.Value.Creator == Context.Message.Author);
			var lobbyVoiceChannel = Context.Guild.VoiceChannels.Where(x => x.Name.StartsWith(Config.VoiceChannelHub)).First();

			if (Context.Message.MentionedUsers.Count > 0)
			{
				if (roomList.Count() > 0)
				{
					var room = Rooms.RoomList[roomList.First().Key];
					var user = (SocketGuildUser)Context.Message.MentionedUsers.First();

					if (room.Private && room.RoleToSeeRoom != null)
					{
						var roles = user.Roles.Where(x => x.Name == room.RoleToSeeRoom.Name);
						if (roles.Count() > 0)
						{
							user.RemoveRoleAsync(room.RoleToSeeRoom);

							if (user.VoiceChannel.Name == room.VoiceChannel.Value.Name)
								user.ModifyAsync(x => x.Channel = lobbyVoiceChannel);

							return ReplyAsync(Strings.PlayerKicked(player, room.VoiceChannel.Value.Name));
						}
						return ReplyAsync(Strings.ErrorCantKickIfNotInPrivateRoom(mention));
					}
					else
					{
						if (user.VoiceChannel.Name == room.VoiceChannel.Value.Name)
						{
							user.ModifyAsync(x => x.Channel = lobbyVoiceChannel);
							return ReplyAsync(Strings.PlayerKicked(player, room.VoiceChannel.Value.Name));
						}
						return ReplyAsync(Strings.ErrorCantKickIfNotInRoom(mention));
					}
				}
				return ReplyAsync(Strings.ErrorRoomNotCreatedYet(mention));
			}
			return ReplyAsync(Strings.ErrorNobodyToKick(mention));
		}
	}
}
