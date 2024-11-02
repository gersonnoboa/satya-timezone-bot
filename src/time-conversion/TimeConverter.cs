using NodaTime;
using NodaTime.Text;

namespace Dev.Noboa
{
	class TimeConverter
	{

		public static List<DateRegion> ConvertToAllTimezones(MessageParameters messageParameters)
		{
			var allDateRegions = new List<DateRegion>();

			var sendingUserRegion = UsernameToTimezoneMapper.Map(messageParameters.UserId);
			var zonedDateTime = ConvertToZonedDateTime(messageParameters.Time, sendingUserRegion.TimeZoneId);
			allDateRegions.Add(new DateRegion(zonedDateTime, sendingUserRegion));

			foreach (var region in Region.AllRegions)
			{
				if (region.Value == sendingUserRegion.Value) { continue; }

				var regionDateTime = ConvertDateTime(zonedDateTime, region.TimeZoneId);
				allDateRegions.Add(new DateRegion(regionDateTime, region));

			}
			return allDateRegions;
		}

		private static ZonedDateTime ConvertToZonedDateTime(string timeString, string timeZoneId)
		{
			// Parse the time string into LocalDateTime
			LocalDateTime localDateTime = ParseLocalDateTime(timeString);

			// Get the timezone info from IANA identifier
			DateTimeZone timeZone = DateTimeZoneProviders.Tzdb[timeZoneId];

			// Convert LocalDateTime to ZonedDateTime
			ZonedDateTime zonedDateTime = localDateTime.InZoneLeniently(timeZone);

			return zonedDateTime;
		}

		private static LocalDateTime ParseLocalDateTime(string timeString)
		{
			// Define possible formats
			var patterns = new[]
			{
				LocalDateTimePattern.CreateWithInvariantCulture("h:mmtt"), // e.g. 9:00PM
				LocalDateTimePattern.CreateWithInvariantCulture("h:mm tt"), // e.g. 9:00 PM
				LocalDateTimePattern.CreateWithInvariantCulture("H:mm"), // e.g. 21:00
				LocalDateTimePattern.CreateWithInvariantCulture("h tt"), // e.g. 9 PM
				LocalDateTimePattern.CreateWithInvariantCulture("htt") // e.g. 9PM
        	};

			foreach (var pattern in patterns)
			{
				var parseResult = pattern.Parse(timeString);
				if (parseResult.Success)
				{
					// Return the successfully parsed LocalDateTime
					return parseResult.Value;
				}
			}

			throw new ArgumentException($"Could not parse the time string {timeString}.");
		}

		private static ZonedDateTime ConvertDateTime(ZonedDateTime dateTime, string timeZoneId)
		{
			DateTimeZone timeZone = DateTimeZoneProviders.Tzdb[timeZoneId];
			return dateTime.WithZone(timeZone);
		}
	}
}
