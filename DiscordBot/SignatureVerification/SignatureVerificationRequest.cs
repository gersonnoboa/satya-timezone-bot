namespace DiscordBot.SignatureVerification;

internal class SignatureVerificationRequest(string signature, string timestamp, string body)
{
    public string Signature { get; set; } = signature;
    public string Timestamp { get; set; } = timestamp;
    public string Body { get; set; } = body;
}