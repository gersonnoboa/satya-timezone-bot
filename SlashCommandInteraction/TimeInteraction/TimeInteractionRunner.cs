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

		try
		{
			var timeInMessage = TimeChecker.ExtractTime(messageParameters.Message);
			if (string.IsNullOrEmpty(timeInMessage))
			{
				var malformedTimeMessage = GenerateMalformedTimeMessage(messageParameters.Message);
				logger.LogWarning($"No time in message: '{messageParameters.Message}'. Will send message: '{malformedTimeMessage}'.");
				return GenerateJsonResult(malformedTimeMessage);
			}

			var allDateRegions = TimeConverter.ConvertToAllTimezones(timeInMessage, messageParameters);
			var message = MessageGenerator.Generate(messageParameters, allDateRegions);

			logger.LogWarning($"Will send message: {message}.");
			return GenerateJsonResult(message);
		}
		catch (Exception e)
		{
			var malformedTimeMessage = GenerateMalformedTimeMessage(messageParameters.Message);
			logger.LogWarning($"Has encountered an error: '{e.Message}'. Will send message: '{malformedTimeMessage}'.");
			return GenerateJsonResult(malformedTimeMessage);
		}
	}

	private static string GenerateMalformedTimeMessage(string message)
	{
		return $"Pon bien la hora. Pusiste: '{message}'.";
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
