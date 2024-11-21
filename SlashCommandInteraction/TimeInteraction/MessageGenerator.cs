using System.Text;
using DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

namespace DiscordBot.SlashCommandInteraction.TimeInteraction;

internal abstract class MessageGenerator
{
	public static string Generate(MessageParameters messageParameters, List<DateRegion> dateRegions)
	{
		var builder = new StringBuilder();

		if (!string.IsNullOrWhiteSpace(messageParameters.Message))
		{
			builder.Append($"{messageParameters.Message}\n");	
		}

		foreach (var dateRegion in dateRegions)
		{
			var time = dateRegion.DateTime.ToString("hh:mm tt");
			builder.Append($"\n{time} en {dateRegion.Region.Value}");
		}

		if (messageParameters.ShouldMentionCurrentChannel)
		{
			builder.Append($"\n\n@{messageParameters.ChannelId}");
		}

		return builder.ToString();
	}
}
