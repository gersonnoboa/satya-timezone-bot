using DiscordBot.PingInteraction;
using DiscordBot.SlashCommandInteraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiscordBot.InteractionRunner;

internal class DiscordInteractionRunner(HttpRequest request, ILogger logger)
{
	public async Task<IActionResult> Run()
	{
		try
		{
			logger.LogInformation("Starting Discord Interaction Runner");
			var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
			logger.LogInformation($"Body: {requestBody}");
			SignatureVerifier.Verify(request, requestBody);
			var messageType = MessageTypeParser.ParseMessageTypeFromBody(requestBody);

			return messageType switch
			{
				MessageType.Ping => PingInteractionRunner.Run(logger),
				MessageType.SlashCommand => SlashCommandInteractionRunner.Run(requestBody, logger),
				_ => new BadRequestResult(),
			};
		}
		catch (SignatureException ex)
		{
			logger.LogInformation(ex.Message);
			return new UnauthorizedResult();
		}
		catch (SlashCommandParserException ex)
		{
			logger.LogInformation(ex.Message);
			return new BadRequestResult();
		}
		catch (Exception ex)
		{
			logger.LogInformation(ex.Message);
			return new BadRequestResult();
		}
	}
}
