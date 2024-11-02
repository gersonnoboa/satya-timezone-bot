using Microsoft.AspNetCore.Mvc;

class PingInteractionRunner
{
	public static JsonResult Run()
	{
		return new JsonResult(new { type = 1 });
	}
}
