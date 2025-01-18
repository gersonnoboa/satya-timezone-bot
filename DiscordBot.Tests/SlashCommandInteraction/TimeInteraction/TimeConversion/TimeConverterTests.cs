using DiscordBot.SlashCommandInteraction.TimeInteraction;
using DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

namespace DiscordBot.Tests.SlashCommandInteraction.TimeInteraction.TimeConversion;

[TestClass]
public class TimeConverterTests
{
    [TestMethod]
    public void TestConvert()
    {
        var dateRegions = TimeConverter.ConvertToAllTimezones(
            "4PM", 
            UsernameToTimezoneMapper.GersonId);
        Assert.AreEqual(dateRegions.Count, Region.AllRegions.Length);
        Assert.AreEqual(dateRegions[0].Region, Region.Estonia);
    }
}