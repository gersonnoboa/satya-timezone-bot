namespace DiscordBot.SlashCommandInteraction.TimeInteraction;

public record MessageParameters(
    string UserId,
    string Time, 
    string? Message
    ) { }
