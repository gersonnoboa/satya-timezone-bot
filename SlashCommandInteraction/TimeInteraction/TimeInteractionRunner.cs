using System.Text.Json;
using DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBot.SlashCommandInteraction.TimeInteraction;

internal abstract class TimeInteractionRunner
{
	public static JsonResult Run(JsonElement root)
	{
		var messageParameters = MessageParametersParser.Parse(root);

		if (!TimeChecker.IsTimeCorrect(messageParameters.Time))
		{
			return GenerateJsonResult($"Pon bien la hora.");
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
