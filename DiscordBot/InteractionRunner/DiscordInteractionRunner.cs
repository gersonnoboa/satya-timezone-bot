using DiscordBot.PingInteraction;
using DiscordBot.SignatureVerification;
using DiscordBot.SlashCommandInteraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiscordBot.InteractionRunner;

internal class DiscordInteractionRunner(
	string requestBody, 
	ISignatureVerifier signatureVerifier, 
	SignatureVerificationRequest signatureVerificationRequest, 
	string? publicKey,
	ILogger logger)
{
	internal Task<IActionResult> Run()
	{
		try
		{
			logger.LogWarning("Starting Discord Interaction Runner");
			logger.LogWarning($"Body: {requestBody}");
			
			signatureVerifier.Verify(signatureVerificationRequest, publicKey);
			
			var messageType = MessageTypeParser.ParseMessageTypeFromBody(requestBody);

			return Task.FromResult(messageType switch
			{
				MessageType.Ping => PingInteractionRunner.Run(logger),
				MessageType.SlashCommand => SlashCommandInteractionRunner.Run(requestBody, logger),
				_ => new BadRequestResult(),
			});
		}
		catch (SignatureException ex)
		{
			logger.LogWarning(ex.Message);
			return Task.FromResult<IActionResult>(new UnauthorizedResult());
		}
		catch (SlashCommandParserException ex)
		{
			logger.LogWarning(ex.Message);
			return Task.FromResult<IActionResult>(new BadRequestResult());
		}
		catch (Exception ex)
		{
			logger.LogWarning(ex.Message);
			return Task.FromResult<IActionResult>(new BadRequestResult());
		}
	}
}
