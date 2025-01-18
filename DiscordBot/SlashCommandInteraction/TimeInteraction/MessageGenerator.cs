using System.Text;
using DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

namespace DiscordBot.SlashCommandInteraction.TimeInteraction;

internal abstract class MessageGenerator
{
	public static string Generate(MessageParameters messageParameters, List<DateCountry> dateCountries)
	{
		var builder = new StringBuilder();

		if (!string.IsNullOrWhiteSpace(messageParameters.Message))
		{
			builder.Append($"{messageParameters.Message}\n");	
		}

		foreach (var country in dateCountries)
		{
			var format = "h:mmtt";
			
			if (country.Regions.Count == 1)
			{
				var time = country.Regions.First().DateTime.ToString(format);
				builder.Append($"\n{country.Country.Value}: {time}");
			}
			else
			{
				builder.Append($"\n{country.Country.Value}: ");
				var times = country.Regions.Select(dateRegion =>
				{
					var time = dateRegion.DateTime.ToString(format);
					return $"{time} {dateRegion.Region.Value}";
				});
				builder.Append(string.Join(", ", times));
			}
		}

		return builder.ToString();
	}
}
