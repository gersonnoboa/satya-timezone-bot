using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiscordBot.SlashCommandInteraction.WolfieTextInteraction;

public abstract class WolfieTextInteractionRunner
{
    public static JsonResult Run(JsonElement root, ILogger logger)
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
