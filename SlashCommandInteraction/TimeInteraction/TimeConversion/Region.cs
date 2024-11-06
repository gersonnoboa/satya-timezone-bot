namespace DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

internal class Region
{
	private Region(string value, string timeZoneId) { Value = value; TimeZoneId = timeZoneId; }

	public string Value { get; private set; }
	public string TimeZoneId { get; private set; }

	public static Region Ecuador { get { return new Region("Ecuador", "America/Guayaquil"); } }
	public static Region UnitedStates { get { return new Region("NY", "America/New_York"); } }
	public static Region UnitedKingdom { get { return new Region("UK", "Europe/London"); } }
	public static Region Estonia { get { return new Region("Estonia", "Europe/Tallinn"); } }
	public static Region Canada { get { return new Region("Canadá", "America/Halifax"); } }
	public static Region[] AllRegions { get { return [Ecuador, UnitedStates, UnitedKingdom, Estonia, Canada]; } }
}
