using DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;

namespace DiscordBot.Tests.SlashCommandInteraction.TimeInteraction.TimeConversion;

[TestClass]
public class TimeCheckerTests
{
    [TestMethod]
    public void TestTimeConversionAloneLowercase()
    {
        var actual = TimeChecker.ExtractTime("4pm");
        Assert.AreEqual("4pm", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionFullUppercase()
    {
        var actual = TimeChecker.ExtractTime("4:30PM");
        Assert.AreEqual("4:30PM", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionAloneFullLowercase()
    {
        var actual = TimeChecker.ExtractTime("4:30pm");
        Assert.AreEqual("4:30pm", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionUppercase()
    {
        var actual = TimeChecker.ExtractTime("4PM");
        Assert.AreEqual("4PM", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionInMessage()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 4pm");
        Assert.AreEqual("4pm", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionShortLowercase()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 4pm");
        Assert.AreEqual("4pm", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionShortUppercase()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 4PM");
        Assert.AreEqual("4PM", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionLongLowercase()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 4:00pm");
        Assert.AreEqual("4:00pm", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionLongUppercase()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 4:00PM");
        Assert.AreEqual("4:00PM", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionLongSpaceLowercase()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 4:00 pm");
        Assert.AreEqual("4:00 pm", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionLongSpaceUppercase()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 4:00 PM");
        Assert.AreEqual("4:00 PM", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionAnyTime()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 4:35 PM");
        Assert.AreEqual("4:35 PM", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionWithoutAmPm()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 4:35");
        Assert.AreEqual("4:35", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionInvalidTime()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 30:35");
        Assert.AreEqual("", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionDots()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 2.30PM");
        Assert.AreEqual("", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionNoTime()
    {
        var actual = TimeChecker.ExtractTime("Pilas");
        Assert.AreEqual("", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionOnlyNumber()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 4");
        Assert.AreEqual("", actual);
    }
    
    [TestMethod]
    public void TestTimeConversionTypo()
    {
        var actual = TimeChecker.ExtractTime("Pilas a las 4:30PMM");
        Assert.AreEqual("", actual);
    }
}