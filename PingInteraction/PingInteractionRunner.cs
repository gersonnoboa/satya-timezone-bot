using Microsoft.AspNetCore.Mvc;

namespace DiscordBot.PingInteraction;

class PingInteractionRunner
{
	public static JsonResult Run()
	{
		return new JsonResult(new { type = 1 });
	}
}
