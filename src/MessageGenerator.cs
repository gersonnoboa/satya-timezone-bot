using System.Text;
using Microsoft.Extensions.Primitives;
using NodaTime;
using NodaTime.Text;

namespace Dev.Noboa;

class MessageGenerator
{
	public static string Generate(MessageParameters messageParameters, List<DateRegion> dateRegions)
	{
		var builder = new StringBuilder();

		var game = string.IsNullOrWhiteSpace(messageParameters.GameName) ? "" : $" {messageParameters.GameName} ";
		builder.Append($"<@{messageParameters.UserId}> quiere jugar{game} a las siguientes horas:");

		foreach (var dateRegion in dateRegions)
		{
			var pattern = ZonedDateTimePattern.CreateWithInvariantCulture("hh:mm tt", DateTimeZoneProviders.Tzdb);
			var time = pattern.Format(dateRegion.DateTime);

			builder.Append($"\n{time} en {dateRegion.Region.Value}");
		}

		if (messageParameters.ShouldMentionCurrentChannel)
		{
			builder.Append($"\n\n<#{messageParameters.ChannelId}");
		}

		return builder.ToString();
	}
}
