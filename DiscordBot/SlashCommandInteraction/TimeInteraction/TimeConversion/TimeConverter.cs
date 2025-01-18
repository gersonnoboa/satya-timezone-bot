using System.Globalization;

namespace DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

internal abstract class TimeConverter
{
	public static List<DateCountry> ConvertToAllTimezones(string time, string userId)
	{
		var allDateCountries = new List<DateCountry>();

		var sendingUserRegion = UsernameToTimezoneMapper.Map(userId);
		var zonedDateTime = ConvertToZonedDateTime(time, sendingUserRegion.TimeZoneId);
		var moveToFirstCountry = false;
		
		foreach (var country in Country.AllCountries)
		{
			var regions = new List<DateRegion>();
			
			foreach (var region in country.Regions)
			{
				if (region.Equals(sendingUserRegion))
				{
					moveToFirstCountry = true;
					regions.Add(new DateRegion(zonedDateTime, sendingUserRegion));
				}
				else
				{
					var regionDateTime = ConvertDateTime(zonedDateTime, region.TimeZoneId);
					var dateRegion = new DateRegion(regionDateTime, region);
					regions.Add(dateRegion);	
				}
			}
			
			var dateCountry = new DateCountry(country, regions);
			if (moveToFirstCountry)
			{
				moveToFirstCountry = false;
				allDateCountries.Insert(0, dateCountry);
			}
			else
			{
				allDateCountries.Add(dateCountry);
			}
		}

		return allDateCountries;
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
