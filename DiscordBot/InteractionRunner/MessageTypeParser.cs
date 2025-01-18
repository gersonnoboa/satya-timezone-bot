using System.Text.Json;

namespace DiscordBot.InteractionRunner;

internal abstract class MessageTypeParser
{
	public static MessageType ParseMessageTypeFromBody(string requestBody)
	{
		using var document = JsonDocument.Parse(requestBody);
		var root = document.RootElement;
		root.TryGetProperty("type", out var typeElement);
		var type = typeElement.GetInt32();

		if (Enum.IsDefined(typeof(MessageType), type)) { return (MessageType)type; }
		else { return MessageType.Unknown; }

	}
}
