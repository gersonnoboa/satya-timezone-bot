namespace DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

internal class Region
{
	private Region(string value, string timeZoneId) { Value = value; TimeZoneId = timeZoneId; }

	public string Value { get; private set; }
	public string TimeZoneId { get; private set; }

	public static Region Ecuador => new("Ecuador", "America/Guayaquil");
	public static Region UnitedStates => new("NY", "America/New_York");
	public static Region UnitedKingdom => new("UK", "Europe/London");
	public static Region Estonia => new("Estonia", "Europe/Tallinn");
	public static Region Canada => new("Canadá", "America/Halifax");
	public static Region[] AllRegions => [Ecuador, UnitedStates, UnitedKingdom, Estonia, Canada];
}
