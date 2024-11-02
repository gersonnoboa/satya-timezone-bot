using System.Text.Json;

namespace Dev.Noboa;
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
		bool shouldMentionCurrentChannel = false;
		string? gameName = null;

		if (dataElement.TryGetProperty("options", out JsonElement optionsElement))
		{
			foreach (var option in optionsElement.EnumerateArray())
			{
				var optionName = option.GetProperty("name").GetString();
				if (optionName == "hora")
				{
					time = option.GetProperty("value").GetString();
				}
				else if (optionName == "mencionar-canal")
				{
					shouldMentionCurrentChannel = option.GetProperty("value").GetBoolean();
				}
				else if (optionName == "juego")
				{
					gameName = option.GetProperty("value").GetString();
				}
			}
		}

		return new MessageParameters(userId, channelId, time ?? "", shouldMentionCurrentChannel, gameName);
	}
}
