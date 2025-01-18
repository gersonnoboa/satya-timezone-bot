using DiscordBot.PingInteraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DiscordBot.Tests.PingInteraction;

[TestClass]
public class PingInteractionRunnerTests
{
    private readonly ILogger<PingInteractionRunner> _logger = NullLogger<PingInteractionRunner>.Instance;
    
    [TestMethod]
    public void TestSuccess()
    {
        var expected = new JsonResult(new { type = 1 });
        var jsonResult = PingInteractionRunner.Run(_logger);
        var actualType = TypeUtils.GetValueFromAnonymousType<int>(jsonResult.Value, "type");
        
        Assert.AreEqual(1, actualType);
    }
}