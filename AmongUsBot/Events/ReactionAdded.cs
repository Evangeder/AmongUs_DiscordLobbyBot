using Discord;
using Discord.WebSocket;
using System.Text;
using System.Threading.Tasks;

namespace AmongUsBot.Events
{
    partial class Events 
    {
        internal Task ReactionAdded(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            if (Rooms.RoomList.ContainsKey(arg3.MessageId))
            {
                Rooms.Room room = Rooms.RoomList[arg3.MessageId];
                string message = Rooms.RoomList[arg3.MessageId].BotMessage.Content;

                var _user = (SocketUser)arg3.User;
                var _userGuild = (SocketGuildUser)arg3.User;

                if (arg3.Emote.Name == Emotes.RedAlive.Name && !room.Users.Contains(_user) && !_user.IsBot && _userGuild.VoiceChannel != null && room.Users.Count < 10)
                {
                    room.Users.Add((SocketUser)arg3.User);
                    _userGuild.ModifyAsync(x => { x.Channel = room.VoiceChannel; });
                    if (message.Contains(Strings.PlayerCount))
                    {
                        string[] messages = message.Replace("\r\n", "\n").Split('\n');

                        var sb = new StringBuilder();
                        sb.AppendLine(messages[0]);
                        sb.AppendLine(messages[1]);
                        sb.AppendLine(messages[2]);
                        sb.Append($"{Strings.PlayerCount}{room.Users.Count}/{room.MaxPlayers} (");
                        foreach (var user in room.Users)
                            sb.Append($"{user.Username}, ");
                        sb.Remove(sb.Length - 2, 2);
                        sb.AppendLine(")");
                        sb.Append(messages[4]);
                        
                        room.BotMessage.ModifyAsync(msg => 
                            msg.Content = sb.ToString().Replace("\r\n", "\n")
                            );
                    }
                }

                if (_userGuild.VoiceChannel == null && !_user.IsBot)
                    _user.SendMessageAsync(Strings.ErrorMustBeInVoiceLobbyReaction(_user.Mention));
            }
            return Task.CompletedTask;
        }
    }
}
