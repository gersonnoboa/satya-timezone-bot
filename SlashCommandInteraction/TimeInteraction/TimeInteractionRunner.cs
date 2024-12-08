using System.Text.Json;
using DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiscordBot.SlashCommandInteraction.TimeInteraction;

internal abstract class TimeInteractionRunner
{
	public static JsonResult Run(JsonElement root, ILogger logger)
	{
		logger.LogWarning("Running SlashCommandInteraction time");
		var messageParameters = MessageParametersParser.Parse(root);
		logger.LogWarning($"Running SlashCommandInteraction time: {JsonSerializer.Serialize(messageParameters)}");
		
		if (!TimeChecker.IsTimeCorrect(messageParameters.Time))
		{
			logger.LogWarning($"Wrong time, {messageParameters.Time}.");
			return GenerateJsonResult($"Pon bien la hora.");
		}

		var allDateRegions = TimeConverter.ConvertToAllTimezones(messageParameters);
		var message = MessageGenerator.Generate(messageParameters, allDateRegions);

		logger.LogWarning($"Will send message: {message}.");
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
