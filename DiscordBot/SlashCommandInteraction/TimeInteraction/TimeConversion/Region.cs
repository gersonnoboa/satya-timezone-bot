namespace DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

internal class Region: IEquatable<Region>
{
	private Region(string value, string timeZoneId) { Value = value; TimeZoneId = timeZoneId; }

	public string Value { get; }
	public string TimeZoneId { get; }

	public static Region Guayaquil => new("Ecuador", "America/Guayaquil");
	public static Region NewYork => new("NY", "America/New_York");
	public static Region London => new("UK", "Europe/London");
	public static Region Tallinn => new("Estonia", "Europe/Tallinn");
	public static Region Halifax => new("Lunenburg", "America/Halifax");
	public static Region StJohns => new("NL", "America/St_Johns");
	public static Region[] AllRegions => [Guayaquil, NewYork, London, Tallinn, Halifax, StJohns];

	public bool Equals(Region? other)
	{
		if (other is null) return false;
		if (ReferenceEquals(this, other)) return true;
		return Value == other.Value && TimeZoneId == other.TimeZoneId;
	}

	public override bool Equals(object? obj)
	{
		if (obj is null) return false;
		if (ReferenceEquals(this, obj)) return true;
		return obj.GetType() == GetType() && Equals((Region)obj);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Value, TimeZoneId);
	}
}
