using DiscordBot.InteractionRunner;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DiscordBot;

public class DiscordBot(ILogger<DiscordBot> logger)
{
    [Function("DiscordInteraction")]
    public async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
    {
        var runner = new DiscordInteractionRunner(req, logger);
        return await runner.Run();
    }
}
