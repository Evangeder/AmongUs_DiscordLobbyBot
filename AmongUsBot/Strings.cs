namespace AmongUsBot
{
	static class Strings
	{
		public static string Help(string mention)
			=> $"Witaj, {mention} na pokładzie naszego statku kosmicznego!\n" +
			"Pozwól mi się przedstawić, nazywam się `AmongUs_Bot` i pomagam w organizacji ekspedycji kosmicznych - tworzenie pokojów głosowych do Among Us.\n" +
			"Jeśli chcesz, mogę dla Ciebie zrobić kilka rzeczy, wystarczy że dasz znać.\n\n" +
			"Odpowiadam na polecenia:\n" +
			"\t- `!yeet @gracz`\n" +
			"\t\t-> Wklejam wyrzucenie gracza ze statku\n" +
			"\t- `!nowa <priv|public> <ilość graczy> <ilość impostorów>`\n" +
			"\t\t-> Tworzy nowy room głosowy do gry.\n" +
			"\t- `!invite @gracz`\n" +
			"\t\t-> Zaprasza gracza do prywatnego lobby.\n" +
			"\t- `!room`\n" +
			"\t\t-> Wyświetla informacje o twoim pokoju.\n" +
			"\t- `!kick @gracz`\n" +
			"\t\t-> Usuwa gracza z pokoju\n\n" +
			"Jeśli o czymś zapomniałem lub zachowuję się dziwnie, możesz poinformować autora: Evander#2079 i/lub wyrzucić mnie ze statku `!yeet`.\n" +
			"Bo wiesz.. ja też mogę być impostorem ;)";

		public static string Yeet(string player, bool impostor, int left)
			=> "　　　。　　　　•　 　ﾟ　　。 　　.\n\n" +
			"　　　.　　　 　　.　　　　　。　　 。　. 　\n\n" +
			".　　 。　　　　　 ඞ 。 . 　　 • 　　　　•\n\n" +
			$"　　ﾟ　 {player} was {(impostor ? "the" : "not An")} Impostor.　 。　.\n\n" +
			$"　　'　　　 {left} Impostor remains 　 　　。\n\n" +
			"　　ﾟ　　　.　　　. ,　　　　.　 .";

		public static string InfoNewRoom
			=> "Użyj `!nowa <priv|pub> <ilość graczy> <ilość impostorów>.`";

		public static string YeetFail(string mention)
			=> $"Ty {mention}, ale kogo mam wywalić...?";

		public static string ErrorRoomNotCreatedYet(string mention)
			=> $"Nie masz założonego pokoju, {mention}.";

		public static string ErrorNobodyToKick(string mention)
			=> $"Um... {mention}, nie oznaczyłeś kogo wyrzucić z rooma.";

		public static string ErrorNobodyToInvite(string mention)
			=> $"Um... {mention}, nie oznaczyłeś kogo zaprosić do rooma.";

		public static string ErrorCantKickIfNotInRoom(string mention)
			=> $"Uh, {mention}.. Nie mogę wyrzucić z kanału kogoś, kogo na nim nie ma, wiesz? ;)";

		public static string ErrorCantKickIfNotInPrivateRoom(string mention)
			=> $"Uh, {mention}.. Nie mogę wyrzucić z kanału kogoś, kto nawet go nie widzi :)";

		public static string PlayerKicked(string player, string channelName)
			=> $"Gracz {Emotes.RedDeadStanding}{player}{Emotes.RedDeadLaid} został wyrzucony z pokoju {channelName}";

		public static string YouveBeenInvited(string owner, string voiceChannelName, string guildName)
			=> $"Gracz {owner} zaprosił cię do pokoju {voiceChannelName} na serwerze {guildName}!";

		public static string InviteSuccess(string mention, string player)
			=> $"Ok {mention}, udostępniono pokój dla gracza {player}.";

		public static string ErrorRoomNotPrivate(string mention)
			=> $"Twój pokój nie jest prywatny, {mention}.";

		public static string RoomInfo(string mention, int userCount, string users, int max, bool priv)
			=> $"Dane twojego pokoju {mention}:\n" +
			$"Gracze: {userCount}/{max}: ({users})\n" +
			$"Typ pokoju: {(priv ? "Prywatny" : "Publiczny")}";

		public static string ErrorPlayerNaN => "Wartość dla ilości graczy nie jest liczbą.";
		public static string ErrorImpostorNaN => "Wartość dla ilości impostorów nie jest liczbą.";

		public static string PrivateRoomCreated(string mention)
			=> $"Ok, {mention}, zostaniesz przeniesiony do nowego pokoju.\n\n" +
			$"Aby zaprosić pozostałych graczy, wpisz `!invite <@user>`.";

		public static string NewRoomAnnouncement(string mention, int max, string users, int impostors)
			=> $"Gracz {mention} tworzy nowe publiczne lobby.\n\n" +
			$"Aby dołączyć, dodaj reakcję {Emotes.RedAlive}.\n" +
			$"Ilość graczy: 1/{max} ({users})\n" +
			$"Ilość impostorów: {impostors}";

		public static string ErrorMustBeInVoiceLobby(string mention)
			=> $"{mention} najpierw musisz dołączyć do pokoju głosowego.";

		public static string ErrorMustBeInVoiceLobbyReaction(string mention)
			=> $"{mention} musisz najpierw wejść do kanału głosowego.\n" +
			$"Usuń reakcję, wejdź na kanał głosowy i spróbuj pownownie :)";

		public static string PlayerCount => "Ilość graczy: ";

		public static string AutoRestartMessageWhenCrash(string mention)
			=> $"Wiadomość automatyczna: {mention}\n" +
			"Bot uległ awarii i musiał zostać zresetowany.\n" +
			"Usunięto wszystkie role i kanały utworzone przez bota.";
	}
}
