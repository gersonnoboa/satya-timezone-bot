
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Dev.Noboa;

public class DiscordBot
{
    private readonly ILogger<DiscordBot> _logger;

    public DiscordBot(ILogger<DiscordBot> logger)
    {
        _logger = logger;
    }

    [Function("DiscordInteraction")]
    public async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
    {
        var runner = new DiscordInteractionRunner(req, _logger);
        return await runner.Run();
    }
}
