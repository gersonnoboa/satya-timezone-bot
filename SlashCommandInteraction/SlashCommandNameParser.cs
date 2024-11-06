using System.Text.Json;

namespace DiscordBot.SlashCommandInteraction;

internal class SlashCommandParserException(string message) : Exception(message) { }

internal abstract class SlashCommandNameParser
{
	public static string Parse(JsonElement root)
	{
		root.TryGetProperty("data", out var dataElement);
		dataElement.TryGetProperty("name", out var nameElement);
		var name = nameElement.GetString();

		if (string.IsNullOrEmpty(name))
		{
			throw new SlashCommandParserException("Invalid command");
		}

		return name;
	}
}
