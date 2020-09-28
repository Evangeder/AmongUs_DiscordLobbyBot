using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace AmongUsBot.Events
{
    partial class Events
    {
        internal Task UserVoiceStateUpdated(SocketUser arg1, SocketVoiceState arg2, SocketVoiceState arg3)
        {
            ulong _categoryId   = 0;
            SocketGuild guild   = _client.Guilds.First();
            var user            = ((SocketGuildUser)arg1);
            var userRoles       = user.Roles.ToList();
            var ingameRole      = guild.Roles.Where(x => x.Name == Config.IngameRank).First();

            foreach (var category in guild.CategoryChannels.ToList())
                if (string.Compare(category.Name, Config.LobbyCategoryName, true) == 0)
                {
                    _categoryId = category.Id;
                    break;
                }

            if (Rooms.RoomList.Count > 0)
                foreach (var channel in guild.VoiceChannels)
                {
                    if (channel.CategoryId != _categoryId) continue;
                    if (channel.Name.StartsWith(Config.VoiceRoomPrefix) && channel.Users.Count == 0)
                    {
                        ulong k = Rooms.RoomList.Where(x => x.Value.VoiceChannel.Value.Id == channel.Id).First().Key;
                        Rooms.RoomList[k].BotMessage.ModifyAsync(msg => msg.Content = "Pokój wygasł.");
                        if (Rooms.RoomList[k].Private) Rooms.RoomList[k].RoleToSeeRoom.DeleteAsync();
                        Rooms.RoomList.Remove(k);
                        channel.DeleteAsync();
                    }
                }

            if (!userRoles.Contains(ingameRole) && user.VoiceChannel != null && (user.VoiceChannel.CategoryId == _categoryId || user.VoiceChannel.CategoryId == null) && user.VoiceChannel.Name.StartsWith("Room "))
                user.AddRoleAsync(ingameRole);
            else if (userRoles.Contains(ingameRole))
                user.RemoveRoleAsync(ingameRole);

            return Task.CompletedTask;
        }
    }
}
