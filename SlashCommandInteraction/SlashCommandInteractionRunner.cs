using System.Text.Json;
using DiscordBot.SlashCommandInteraction.TimeInteraction;
using Microsoft.AspNetCore.Mvc;

namespace DiscordBot.SlashCommandInteraction;

internal abstract class SlashCommandInteractionRunner
{
	public static IActionResult Run(string requestBody)
	{
		using var document = JsonDocument.Parse(requestBody);
		var root = document.RootElement;
		var slashCommandName = SlashCommandNameParser.Parse(root);

		return slashCommandName switch
		{
			"hora" => TimeInteractionRunner.Run(root),
			_ => new BadRequestResult(),
		};
	}
}
