namespace DiscordBot.SlashCommandInteraction.TimeInteraction;

record MessageParameters(string UserId, string ChannelId, string Time, bool ShouldMentionCurrentChannel, string? Message) { }
