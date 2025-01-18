using DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

namespace DiscordBot.Tests.SlashCommandInteraction.TimeInteraction.TimeConversion;

[TestClass]
public class UsernameToTimezoneMapperTests
{
    [TestMethod]
    public void TestGerson()
    {
        var actual = UsernameToTimezoneMapper.Map(UsernameToTimezoneMapper.GersonId);
        Assert.AreEqual(Region.Estonia, actual);
    }

    [TestMethod]
    public void TestJaje()
    {
        var actual = UsernameToTimezoneMapper.Map(UsernameToTimezoneMapper.JajeId);
        Assert.AreEqual(Region.UnitedKingdom, actual);
    }

    [TestMethod]
    public void TestWilson()
    {
        var actual = UsernameToTimezoneMapper.Map(UsernameToTimezoneMapper.WilsonId);
        Assert.AreEqual(Region.UnitedStates, actual);
    }

    [TestMethod]
    public void TestMuertoCa()
    {
        var actual = UsernameToTimezoneMapper.Map(UsernameToTimezoneMapper.MuertoCaId);
        Assert.AreEqual(Region.CanadaHalifax, actual);
    }

    [TestMethod]
    public void TestMuertoEu()
    {
        var actual = UsernameToTimezoneMapper.Map(UsernameToTimezoneMapper.MuertoEuId);
        Assert.AreEqual(Region.CanadaHalifax, actual);
    }

    [TestMethod]
    public void TestBeto()
    {
        var actual = UsernameToTimezoneMapper.Map(UsernameToTimezoneMapper.BetoId);
        Assert.AreEqual(Region.CanadaStJohns, actual);
    }

    [TestMethod]
    public void TestEcuador()
    {
        var actual = UsernameToTimezoneMapper.Map("id");
        Assert.AreEqual(Region.Ecuador, actual);
    }
}