using Discord.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmongUsBot.Commands
{
	public class RoomInfoModule : ModuleBase<SocketCommandContext>
	{
		[Command("room")]
		public Task SayAsync()
		{
			var roomList	= Rooms.RoomList.Where(x => x.Value.Creator == Context.Message.Author);
			var mention		= Context.Message.Author.Mention;

			if (roomList.Count() > 0)
			{
				var room = Rooms.RoomList[roomList.First().Key];

				var sb = new StringBuilder();
				foreach (var user in room.Users)
					sb.Append($"{user.Username}, ");
				sb.Remove(sb.Length - 2, 2);

				return ReplyAsync(Strings.RoomInfo(mention, room.Users.Count, sb.ToString(), room.MaxPlayers, room.Private));
			}
			return ReplyAsync(Strings.ErrorRoomNotCreatedYet(mention));
		}
	}
}
