using System.Text.Json;

namespace DiscordBot.SlashCommandInteraction.TimeInteraction;

public abstract class MessageParametersParser
{
	public static MessageParameters Parse(JsonElement root)
	{
		// User
		var userElement = root.GetProperty("member").GetProperty("user");
		var userId = userElement.GetProperty("id").ToString();

		// Data
		var dataElement = root.GetProperty("data");

		string? time = null;
		string? message = null;

		if (dataElement.TryGetProperty("options", out var optionsElement))
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
				}
			}
		}

		return new MessageParameters(userId, time ?? "", message);
	}
}
