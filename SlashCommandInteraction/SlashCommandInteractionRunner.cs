using System.Text.Json;
using DiscordBot.SlashCommandInteraction.TimeInteraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiscordBot.SlashCommandInteraction;

internal abstract class SlashCommandInteractionRunner
{
	public static IActionResult Run(string requestBody, ILogger logger)
	{
		using var document = JsonDocument.Parse(requestBody);
		var root = document.RootElement;
		var slashCommandName = SlashCommandNameParser.Parse(root);
		
		logger.LogInformation($"Slash command name: {slashCommandName}");
		
		return slashCommandName switch
		{
			"hora" => TimeInteractionRunner.Run(root, logger),
			_ => new BadRequestResult(),
		};
	}
}
