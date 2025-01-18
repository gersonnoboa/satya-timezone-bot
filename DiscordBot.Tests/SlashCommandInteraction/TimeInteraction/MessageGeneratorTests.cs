using DiscordBot.SlashCommandInteraction.TimeInteraction;
using DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;
using Google.Protobuf;

namespace DiscordBot.Tests.SlashCommandInteraction.TimeInteraction;

[TestClass]
public class MessageGeneratorTests
{
    [TestMethod]
    public void TestGeneratedMessage()
    {
        var userId = UsernameToTimezoneMapper.GersonId;
        var message = "Pilas a las 4PM";
        var time = TimeChecker.ExtractTime(message);
        var messageParameters = new MessageParameters(userId, message);
        var dateRegions = TimeConverter.ConvertToAllTimezones(time, userId);
        var generatedMessage = MessageGenerator.Generate(messageParameters, dateRegions);

        var splitMessage = generatedMessage.Split("\n");
        Assert.IsTrue(generatedMessage.StartsWith(message));
        Assert.AreEqual(8, splitMessage.Length);

        for (int i = 0; i < dateRegions.Count; i++)
        {
            var dateRegion = dateRegions[i];
            var line = splitMessage[i + 2];
            var formattedTime = dateRegion.DateTime.ToString("h:mm tt");
            Assert.AreEqual($"{formattedTime} en {dateRegion.Region.Value}", line);
        }
    }
}