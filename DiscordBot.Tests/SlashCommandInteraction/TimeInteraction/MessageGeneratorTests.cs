using DiscordBot.SlashCommandInteraction.TimeInteraction;
using DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

namespace DiscordBot.Tests.SlashCommandInteraction.TimeInteraction;

[TestClass]
public class MessageGeneratorTests
{
    [TestMethod]
    public void TestGeneratedMessage()
    {
        const string userId = UsernameToTimezoneMapper.JajeId;
        const string message = "Pilas a las 4PM";
        
        var time = TimeChecker.ExtractTime(message);
        var messageParameters = new MessageParameters(userId, message);
        var dateCountries = TimeConverter.ConvertToAllTimezones(time, userId);
        var generatedMessage = MessageGenerator.Generate(messageParameters, dateCountries);

        var splitMessage = generatedMessage.Split("\n");
        Assert.IsTrue(generatedMessage.StartsWith(message));
        Assert.AreEqual(Country.AllCountries.Length + 2, splitMessage.Length);
        
        Console.WriteLine(generatedMessage);
        for (var i = 0; i < dateCountries.Count; i++)
        {
            var dateCountry = dateCountries[i];
            var line = splitMessage[i + 2];

            Assert.IsTrue(line.StartsWith($"{dateCountry.Country.Value}"));

            if (dateCountry.Regions.Count > 1)
            {
                Assert.IsTrue(line.Contains(','));
            }
        }
    }
}