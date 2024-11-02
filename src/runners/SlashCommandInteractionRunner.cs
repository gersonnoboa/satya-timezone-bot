using System.Text.Json;
using Dev.Noboa;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Noboa;

class SlashCommandInteractionRunner()
{
	static public IActionResult Run(string requestBody)
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
