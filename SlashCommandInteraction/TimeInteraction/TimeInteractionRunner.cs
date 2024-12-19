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
			if (!TimeChecker.IsTimeCorrect(messageParameters.Time))
			{
				var malformedTimeMessage = GenerateMalformedTimeMessage(messageParameters.Time); 
				logger.LogWarning($"Wrong time: '{messageParameters.Time}'. Will send message: '{malformedTimeMessage}'.");
				return GenerateJsonResult(malformedTimeMessage);
			}

			var allDateRegions = TimeConverter.ConvertToAllTimezones(messageParameters);
			var message = MessageGenerator.Generate(messageParameters, allDateRegions);

			logger.LogWarning($"Will send message: {message}.");
			return GenerateJsonResult(message);
		}
		catch (Exception e)
		{
			var malformedTimeMessage = GenerateMalformedTimeMessage(messageParameters.Time); 
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
