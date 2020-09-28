namespace AmongUsBot
{
    static class Config
    {
		public static string[] PrivateKeywords
			=> new string[]
			{
				"priv",
				"private",
				"prv",
				"prywatny",
				"prywatna",
				"pr",
			};

		public static char		CommandPrefix				=> '!';
		public static string	VoiceChannelHub				=> "Poczekalnia na Bota";
		public static string	IngameRank					=> "W Grze";
		public static string	LobbyCategoryName			=> "lobby";
		public static string	VoiceRoomPrefix				=> "Room-";
		public static string	VoiceRoomName(int number)	=> $"{VoiceRoomPrefix}{number}";
	}
}
