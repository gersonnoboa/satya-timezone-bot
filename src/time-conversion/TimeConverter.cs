using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Noboa
{
	class TimeConverter
	{
		public static JsonResult Convert(JsonElement root)
		{
			var messageParameters = MessageParametersParser.Parse(root);

			return new JsonResult(new
			{
				type = 4,
				data = new
				{
					content = $"You said: {messageParameters.Time}, game: {messageParameters.GameName}, shouldMention:{messageParameters.ShouldMentionCurrentChannel}."
				}
			});
		}
	}
}
