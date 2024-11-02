using System.Text.Json;

namespace Dev.Noboa
{
	record MessageParameters(string Time, bool ShouldMentionCurrentChannel, string? GameName) { }

	class MessageParametersParser
	{
		public static MessageParameters Parse(JsonElement root)
		{
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
					else if (optionName == "mencionarCanal")
					{
						shouldMentionCurrentChannel = option.GetProperty("value").GetBoolean();
					}
					else if (optionName == "juego")
					{
						gameName = option.GetProperty("value").GetString();
					}
				}
			}

			return new MessageParameters(time ?? "", shouldMentionCurrentChannel, gameName);
		}
	}

}
