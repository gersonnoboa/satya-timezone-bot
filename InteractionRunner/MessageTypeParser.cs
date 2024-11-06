
using System.Text.Json;

namespace Dev.Noboa;

class MessageTypeParser
{
	public static MessageType ParseMessageTypeFromBody(string requestBody)
	{
		using var document = JsonDocument.Parse(requestBody);
		var root = document.RootElement;
		root.TryGetProperty("type", out JsonElement typeElement);
		var type = typeElement.GetInt32();

		if (Enum.IsDefined(typeof(MessageType), type)) { return (MessageType)type; }
		else { return MessageType.Unknown; }

	}
}
