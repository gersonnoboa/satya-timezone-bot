using Microsoft.AspNetCore.Mvc;

namespace Dev.Noboa;

class PingInteractionRunner
{
	public static JsonResult Run()
	{
		return new JsonResult(new { type = 1 });
	}
}
