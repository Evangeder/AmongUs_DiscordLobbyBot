using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace AmongUsBot.Commands
{
	public class InviteModule : ModuleBase<SocketCommandContext>
	{
		[Command("invite")]
		public Task SayAsync(string player)
		{
			string mention			= Context.Message.Author.Mention;

			if (Rooms.RoomList.Count <= 0)
				return ReplyAsync(Strings.ErrorRoomNotCreatedYet(mention));

			var lobbyVoiceChannel	= Context.Guild.VoiceChannels.Where(x => x.Name.StartsWith(Config.VoiceChannelHub)).First();
			var roomList			= Rooms.RoomList.Where(x => x.Value.Creator == Context.Message.Author);
			var room				= Rooms.RoomList[roomList.FirstOrDefault().Key];

			if (Context.Message.MentionedUsers.Count > 0)
			{
				if (Rooms.RoomList.Where(x => x.Value.Creator == Context.Message.Author).Count() > 0)
				{
					if (room.Private && room.RoleToSeeRoom != null)
					{
						var user = (SocketGuildUser)Context.Message.MentionedUsers.First();
						user.AddRoleAsync(room.RoleToSeeRoom);

						if (user.VoiceChannel == lobbyVoiceChannel)
							user.ModifyAsync(x => x.Channel = room.VoiceChannel);
						else
							user.SendMessageAsync(Strings.YouveBeenInvited(mention, room.VoiceChannel.Value.Name, room.VoiceChannel.Value.Guild.Name));

						return ReplyAsync(Strings.InviteSuccess(mention, player));
					}
					return ReplyAsync(Strings.ErrorRoomNotPrivate(mention));
				}
				return ReplyAsync(Strings.ErrorRoomNotCreatedYet(mention));
			}
			return ReplyAsync(Strings.ErrorNobodyToInvite(mention));
		}
	}
}
