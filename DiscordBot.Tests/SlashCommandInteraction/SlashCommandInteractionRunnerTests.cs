using System.Text.Json;
using DiscordBot.SlashCommandInteraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace DiscordBot.Tests.SlashCommandInteraction;

[TestClass]
public class SlashCommandInteractionRunnerTests
{
    [TestMethod]
    public void TestWinCommand()
    {
        var document = GetJsonDocument("g");
        var jsonResult = (JsonResult)SlashCommandInteractionRunner.Run(document, NullLogger<SlashCommandInteractionRunnerTests>.Instance);
        var data = TypeUtils.GetValueFromAnonymousType<object>(jsonResult.Value, "data");
        var actualContent = TypeUtils.GetValueFromAnonymousType<string>(data, "content");
        
        Assert.AreEqual("Qué será ganar?", actualContent);
    }
    
    [TestMethod]
    public void TestLoseCommand()
    {
        var document = GetJsonDocument("p");
        var jsonResult = (JsonResult)SlashCommandInteractionRunner.Run(document, NullLogger<SlashCommandInteractionRunnerTests>.Instance);
        var data = TypeUtils.GetValueFromAnonymousType<object>(jsonResult.Value, "data");
        var actualContent = TypeUtils.GetValueFromAnonymousType<string>(data, "content");
        
        Assert.AreEqual("Qué será perder?", actualContent);
    }
    
    [TestMethod]
    public void TestWolfieCommand()
    {
        var document = GetJsonDocument("w");
        var jsonResult = (JsonResult)SlashCommandInteractionRunner.Run(document, NullLogger<SlashCommandInteractionRunnerTests>.Instance);
        var data = TypeUtils.GetValueFromAnonymousType<object>(jsonResult.Value, "data");
        var actualContent = TypeUtils.GetValueFromAnonymousType<string>(data, "content");
        
        Assert.AreEqual("Qué será jugar con Wolfie?", actualContent);
    }
    
    private string GetJsonDocument(string commandName)
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
}