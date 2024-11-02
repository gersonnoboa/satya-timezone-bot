using System.Text.Json;

namespace Dev.Noboa
{
	class SlashCommandParserException(string message) : Exception(message) { }
	class SlashCommandNameParser
	{
		public static string Parse(JsonElement root)
		{
			root.TryGetProperty("data", out JsonElement dataElement);
			dataElement.TryGetProperty("name", out JsonElement nameElement);
			var name = nameElement.GetString();

			if (string.IsNullOrEmpty(name))
			{
				throw new SlashCommandParserException("Invalid command");
			}

			return name;
		}
	}
}
