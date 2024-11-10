using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiscordBot.PingInteraction;

internal abstract class PingInteractionRunner
{
	public static JsonResult Run(ILogger logger)
	{
		logger.LogInformation("Running PingInteraction");
		return new JsonResult(new { type = 1 });
	}
}
