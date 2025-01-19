using Microsoft.AspNetCore.Mvc;

namespace DiscordBot.SlashCommandInteraction.SimpleTextInteractions;

public abstract class WinTextInteractionRunner
{
    public static JsonResult Run()
    {
        return GenerateJsonResult("Qué será ganar?");
    }

    private static JsonResult GenerateJsonResult(string message)
    {
        return new JsonResult(new
        {
            type = 4,
            data = new
            {
                content = message
            }
        });
    }
}