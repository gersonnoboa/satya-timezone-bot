using System.Globalization;

namespace DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

internal abstract class TimeConverter
{
	public static List<DateRegion> ConvertToAllTimezones(MessageParameters messageParameters)
	{
		var allDateRegions = new List<DateRegion>();

		var sendingUserRegion = UsernameToTimezoneMapper.Map(messageParameters.UserId);
		var zonedDateTime = ConvertToZonedDateTime(messageParameters.Time, sendingUserRegion.TimeZoneId);
		allDateRegions.Add(new DateRegion(zonedDateTime, sendingUserRegion));

		allDateRegions.AddRange(
			from region in Region.AllRegions 
			where region.Value != sendingUserRegion.Value 
			let regionDateTime = ConvertDateTime(zonedDateTime, region.TimeZoneId) 
			select new DateRegion(regionDateTime, region)
			);
		return allDateRegions;
	}

	private static DateTimeOffset ConvertToZonedDateTime(string timeString, string timeZoneId)
	{
		// Define possible formats
		var patterns = new[]
		{
			"h:mmtt",
			"h:mm tt",
			"H:mm",
			"h tt",
			"htt"
		};

		foreach (var pattern in patterns)
		{
			DateTime.TryParseExact(timeString, pattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out var localDateTime);
			if (localDateTime == DateTime.MinValue) continue;
			
			localDateTime = DateTime.Today.Add(localDateTime.TimeOfDay);
			localDateTime = DateTime.SpecifyKind(localDateTime, DateTimeKind.Unspecified);
			var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
			var dateTimeOffset = new DateTimeOffset(localDateTime, timeZone.GetUtcOffset(localDateTime));

			return dateTimeOffset;
		}

		throw new ArgumentException($"Could not parse the time string: '{timeString}'.");
	}

	private static DateTimeOffset ConvertDateTime(DateTimeOffset dateTime, string timeZoneId)
	{
		var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
		var dateTimeOffset = TimeZoneInfo.ConvertTime(dateTime, timeZone);

		return dateTimeOffset;
	}
}
