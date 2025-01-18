using System.Text.Json;
using DiscordBot.SlashCommandInteraction;
using DiscordBot.SlashCommandInteraction.TimeInteraction.TimeConversion;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace DiscordBot.Tests.SlashCommandInteraction;

[TestClass]
public class SlashCommandInteractionRunnerTests
{
    [TestMethod]
    public void TestTimeCommand()
    {
        const string message = "4PM";
        var document = GetMessageJsonDocument("hora", message);
        var jsonResult = (JsonResult)SlashCommandInteractionRunner.Run(document, NullLogger<SlashCommandInteractionRunnerTests>.Instance);
        var data = TypeUtils.GetValueFromAnonymousType<object>(jsonResult.Value, "data");
        var actualContent = TypeUtils.GetValueFromAnonymousType<string>(data, "content");
        
        Assert.IsTrue(actualContent!.StartsWith(message));
    }
    
    [TestMethod]
    public void TestWinCommand()
    {
        var document = GetSimpleJsonDocument("g");
        var jsonResult = (JsonResult)SlashCommandInteractionRunner.Run(document, NullLogger<SlashCommandInteractionRunnerTests>.Instance);
        var data = TypeUtils.GetValueFromAnonymousType<object>(jsonResult.Value, "data");
        var actualContent = TypeUtils.GetValueFromAnonymousType<string>(data, "content");
        
        Assert.AreEqual("Qué será ganar?", actualContent);
    }
    
    [TestMethod]
    public void TestLoseCommand()
    {
        var document = GetSimpleJsonDocument("p");
        var jsonResult = (JsonResult)SlashCommandInteractionRunner.Run(document, NullLogger<SlashCommandInteractionRunnerTests>.Instance);
        var data = TypeUtils.GetValueFromAnonymousType<object>(jsonResult.Value, "data");
        var actualContent = TypeUtils.GetValueFromAnonymousType<string>(data, "content");
        
        Assert.AreEqual("Qué será perder?", actualContent);
    }
    
    [TestMethod]
    public void TestWolfieCommand()
    {
        var document = GetSimpleJsonDocument("w");
        var jsonResult = (JsonResult)SlashCommandInteractionRunner.Run(document, NullLogger<SlashCommandInteractionRunnerTests>.Instance);
        var data = TypeUtils.GetValueFromAnonymousType<object>(jsonResult.Value, "data");
        var actualContent = TypeUtils.GetValueFromAnonymousType<string>(data, "content");
        
        Assert.AreEqual("Qué será jugar con Wolfie?", actualContent);
    }
    
    private static string GetSimpleJsonDocument(string commandName)
    {
        var obj = new
        {
            data = new
            {
                name = commandName
            },
        };
        
        return JsonSerializer.Serialize(obj);
    }
    
    private static string GetMessageJsonDocument(string commandName, string message)
    {
        var payload = new
        {
            member = new
            {
                user = new
                {
                    id = UsernameToTimezoneMapper.GersonId
                }
            },
            data = new
            {
                options = new[] 
                {
                    new { name = "mensaje", value = message }
                },
                name = commandName
            }
        };
        
        return JsonSerializer.Serialize(payload);
    }
}