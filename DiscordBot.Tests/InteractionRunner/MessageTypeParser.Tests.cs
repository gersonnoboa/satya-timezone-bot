using DiscordBot.InteractionRunner;

namespace DiscordBot.Tests.InteractionRunner;

[TestClass]
public class MessageTypeParserTests
{
    [TestMethod]
    public void ReturnsPing()
    {
        const string body = """{"type":1}""";
        var actual = MessageTypeParser.ParseMessageTypeFromBody(body);
        Assert.AreEqual(MessageType.Ping, actual);
    }
    
    [TestMethod]
    public void ReturnsSlashCommand()
    {
        const string body = """{"type":2}""";
        var actual = MessageTypeParser.ParseMessageTypeFromBody(body);
        Assert.AreEqual(MessageType.SlashCommand, actual);
    }
    
    [TestMethod]
    public void ReturnsSlashUnknown()
    {
        const string body = """{"type":3}""";
        var actual = MessageTypeParser.ParseMessageTypeFromBody(body);
        Assert.AreEqual(MessageType.Unknown, actual);
    }
}