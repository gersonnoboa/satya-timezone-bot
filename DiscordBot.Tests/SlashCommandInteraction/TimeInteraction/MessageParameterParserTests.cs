using System.Text.Json;
using DiscordBot.SlashCommandInteraction.TimeInteraction;

namespace DiscordBot.Tests.SlashCommandInteraction.TimeInteraction;

[TestClass]
public class MessageParameterParserTests
{
    [TestMethod]
    public void TestParsesMessageParameters()
    {
        const string userId = "userId";
        const string message = "message";
        var payload = new
        {
            member = new
            {
                user = new
                {
                    id = userId
                }
            },
            data = new
            {
                options = new[] 
                {
                    new { name = "mensaje", value = message }
                }
            }
        };

        var jsonDocument = JsonSerializer.SerializeToDocument(payload);
        var parameters = MessageParametersParser.Parse(jsonDocument.RootElement);
        
        Assert.AreEqual(userId, parameters.UserId);
        Assert.AreEqual(message, parameters.Message);
    }
    
    [TestMethod]
    public void TestParsesMessageParametersWithNoOptions()
    {
        const string userId = "userId";
        
        var payload = new
        {
            member = new
            {
                user = new
                {
                    id = userId
                }
            },
            data = new
            {
            }
        };

        var jsonDocument = JsonSerializer.SerializeToDocument(payload);
        var parameters = MessageParametersParser.Parse(jsonDocument.RootElement);
        
        Assert.AreEqual(userId, parameters.UserId);
        Assert.AreEqual(null, parameters.Message);
    }
}