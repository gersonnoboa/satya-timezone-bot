namespace Dev.Noboa
{
	record MessageParameters(string UserId, string ChannelId, string Time, bool ShouldMentionCurrentChannel, string? GameName) { }
}
