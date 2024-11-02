using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Noboa;

class TimeInteractionRunner
{
	public static JsonResult Run(JsonElement root)
	{
		var messageParameters = MessageParametersParser.Parse(root);

		if (!TimeChecker.IsTimeCorrect(messageParameters.Time))
		{
			return GenerateJsonResult($"Pon bien la hora, pusiste {messageParameters.Time}.");
		}

		var allDateRegions = TimeConverter.ConvertToAllTimezones(messageParameters);
		var message = MessageGenerator.Generate(messageParameters, allDateRegions);

		return GenerateJsonResult(message);
	}

	private static JsonResult GenerateJsonResult(string message)
	{
		return new JsonResult(new
		{
			type = 4,
			data = new
			{
				content = message
			}
		});
	}
}
