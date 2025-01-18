using DiscordBot.InteractionRunner;
using DiscordBot.SignatureVerification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DiscordBot;

public class DiscordBot(ILogger<DiscordBot> logger)
{
    [Function("DiscordInteraction")]
    public async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var signatureVerifier = new SignatureVerifier();
        var signatureVerificationRequest = SignatureVerificationRequest(req, requestBody);
        var publicKey = Environment.GetEnvironmentVariable("DISCORD_PUBLIC_KEY");
        
        var runner = new DiscordInteractionRunner(
            requestBody, 
            signatureVerifier, 
            signatureVerificationRequest, 
            publicKey,
            logger);
        
        return await runner.Run();
    }

    private SignatureVerificationRequest SignatureVerificationRequest(HttpRequest request, string requestBody)
    {
        var signature = request.Headers["x-signature-ed25519"].ToString();
        var timestamp = request.Headers["x-signature-timestamp"].ToString();

        return new SignatureVerificationRequest(signature, timestamp, requestBody);
    }
}
