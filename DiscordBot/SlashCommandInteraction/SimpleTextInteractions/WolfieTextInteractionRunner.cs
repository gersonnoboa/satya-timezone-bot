using Microsoft.AspNetCore.Mvc;

namespace DiscordBot.SlashCommandInteraction.SimpleTextInteractions;

public abstract class WolfieTextInteractionRunner
{
    public static JsonResult Run()
    {
        return GenerateJsonResult("Qué será jugar con Wolfie?");
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
