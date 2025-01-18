using System.Text.RegularExpressions;

namespace DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

internal abstract partial class TimeChecker
{
	public static string ExtractTime(string time)
	{
		return TimeRegex().Match(time).ToString();
	}

    [GeneratedRegex(@"\b((1[0-2]|0?[1-9]):([0-5][0-9]) ?([AaPp][Mm])?|([01]?[0-9]|2[0-3]):[0-5][0-9]|(1[0-2]|0?[1-9]) ?([AaPp][Mm]))\b")]
    private static partial Regex TimeRegex();
}
