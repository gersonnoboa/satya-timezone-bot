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
        var actual = PingInteractionRunner.Run(_logger);
        
        Assert.AreEqual(expected.ToString(), actual.ToString());
    }
}