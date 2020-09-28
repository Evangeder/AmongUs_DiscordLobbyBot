using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmongUsBot
{
    static class Rooms
    {
        public struct Room
        {
            public int MaxPlayers;
            public List<SocketUser> Users;
            public SocketUser Creator;
            public IUserMessage BotMessage;
            public Optional<IVoiceChannel> VoiceChannel;
            public bool Private;
            public IRole RoleToSeeRoom;
        }

        public static Dictionary<ulong, Room> RoomList = new Dictionary<ulong, Room>();

        public static int Count = 0;
        public static void Register(SocketUser user, IUserMessage message, int maxPlayers, Optional<IVoiceChannel> vChan, bool priv = false, IRole role = null)
        {
            RoomList.Add(message.Id, new Room
            {
                BotMessage = message,
                Creator = user,
                Users = new List<SocketUser>() { user },
                MaxPlayers = maxPlayers,
                VoiceChannel = vChan,
                Private = priv,
                RoleToSeeRoom = role
            });
            Count++;
        }
    }
}
