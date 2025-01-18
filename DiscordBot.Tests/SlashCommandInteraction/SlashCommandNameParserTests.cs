using System.Text.Json;
using DiscordBot.SlashCommandInteraction;

namespace DiscordBot.Tests.SlashCommandInteraction;

[TestClass]
public class SlashCommandNameParserTests
{
    [TestMethod]
    public void TestParsesCommandName()
    {
        const string commandName = "test";
        var obj = new
        {
            data = new
            {
                name = commandName
            },
        };

        var jsonDocument = JsonSerializer.SerializeToDocument(obj);

        var actual = SlashCommandNameParser.Parse(jsonDocument.RootElement);
        Assert.AreEqual("test", actual);
    }
    
    [TestMethod]
    [ExpectedException(typeof(SlashCommandParserException))]
    public void TestFailsWithEmptyCommandName()
    {
        const string commandName = "";
        var obj = new
        {
            data = new
            {
                name = commandName
            },
        };

        var jsonDocument = JsonSerializer.SerializeToDocument(obj);

        SlashCommandNameParser.Parse(jsonDocument.RootElement);
    }
}