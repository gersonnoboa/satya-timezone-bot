using Microsoft.AspNetCore.Mvc;

namespace DiscordBot.SlashCommandInteraction.SimpleTextInteractions;

public abstract class LoseTextInteractionRunner
{
    public static JsonResult Run()
    {
        return GenerateJsonResult("Qué será perder?");
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
