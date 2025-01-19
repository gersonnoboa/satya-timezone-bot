using DiscordBot.SlashCommandInteraction.SimpleTextInteractions;

namespace DiscordBot.Tests.SlashCommandInteraction.SimpleTextInteractions;

[TestClass]
public class LoseTextInteractionRunnerTests
{
    [TestMethod]
    public void TestLoseTextInteractionRunner()
    {
        var jsonResult = LoseTextInteractionRunner.Run();
        var actualType = TypeUtils.GetValueFromAnonymousType<int>(jsonResult.Value, "type");
        var data = TypeUtils.GetValueFromAnonymousType<object>(jsonResult.Value, "data");
        var actualContent = TypeUtils.GetValueFromAnonymousType<string>(data, "content");
        
        Assert.AreEqual(4, actualType);
        Assert.AreEqual("Qué será perder?", actualContent);
    }
}