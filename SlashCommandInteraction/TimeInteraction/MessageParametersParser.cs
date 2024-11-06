using System.Text.Json;

namespace DiscordBot.SlashCommandInteraction.TimeInteraction;
class MessageParametersParser
{
	public static MessageParameters Parse(JsonElement root)
	{
		// User
		var userElement = root.GetProperty("member").GetProperty("user");
		var userId = userElement.GetProperty("id").ToString();

		// Channel
		var channelElement = root.GetProperty("channel");
		var channelId = channelElement.GetProperty("id").ToString();

		// Data
		var dataElement = root.GetProperty("data");

		string? time = null;
		var shouldMentionCurrentChannel = false;
		string? message = null;

		if (dataElement.TryGetProperty("options", out JsonElement optionsElement))
		{
			foreach (var option in optionsElement.EnumerateArray())
			{
				var optionName = option.GetProperty("name").GetString();
				switch (optionName)
				{
					case "hora":
						time = option.GetProperty("value").GetString();
						break;
					case "mensaje":
						message = option.GetProperty("value").GetString();
						break;
					case "mencionar-canal":
						shouldMentionCurrentChannel = option.GetProperty("value").GetBoolean();
						break;
				}
			}
		}

		return new MessageParameters(userId, channelId, time ?? "", shouldMentionCurrentChannel, message);
	}
}
