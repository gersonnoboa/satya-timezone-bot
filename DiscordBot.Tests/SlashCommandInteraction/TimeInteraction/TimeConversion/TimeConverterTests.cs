using DiscordBot.SlashCommandInteraction.TimeInteraction;
using DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

namespace DiscordBot.Tests.SlashCommandInteraction.TimeInteraction.TimeConversion;

[TestClass]
public class TimeConverterTests
{
    [TestMethod]
    public void TestConvert()
    {
        var dateCountries = TimeConverter.ConvertToAllTimezones(
            "4PM", 
            UsernameToTimezoneMapper.JajeId);
        
        Assert.AreEqual(dateCountries.Count, Country.AllCountries.Length);
        Assert.AreEqual(dateCountries[0].Country, Country.UnitedKingdom);
        Assert.AreEqual(dateCountries[0].Regions.Count, 1);
    }
    
    [TestMethod]
    public void TestConvertCountryWithDoubleRegion()
    {
        var dateCountries = TimeConverter.ConvertToAllTimezones(
            "4PM", 
            UsernameToTimezoneMapper.MuertoCaId);
        
        Assert.AreEqual(dateCountries.Count, Country.AllCountries.Length);
        Assert.AreEqual(dateCountries[0].Country, Country.Canada);
        Assert.AreEqual(dateCountries[0].Regions.Count, 2);
    }
}