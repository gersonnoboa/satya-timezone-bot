namespace DiscordBot.SlashCommandInteraction.TimeInteraction;

public record MessageParameters(
    string UserId, 
    string ChannelId, 
    string Time, 
    bool ShouldMentionCurrentChannel, 
    string? Message
    ) { }
