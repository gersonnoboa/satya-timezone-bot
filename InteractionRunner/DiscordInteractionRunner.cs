using DiscordBot.PingInteraction;
using DiscordBot.SlashCommandInteraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DiscordBot.InteractionRunner;

class DiscordInteractionRunner(HttpRequest request, ILogger logger)
{
	public async Task<IActionResult> Run()
	{
		try
		{
			string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
			SignatureVerifier.Verify(request, requestBody);
			var messageType = MessageTypeParser.ParseMessageTypeFromBody(requestBody);

			return messageType switch
			{
				MessageType.Ping => PingInteractionRunner.Run(),
				MessageType.SlashCommand => SlashCommandInteractionRunner.Run(requestBody),
				_ => new BadRequestResult(),
			};
		}
		catch (SignatureException ex)
		{
			logger.LogError(ex.Message);
			return new UnauthorizedResult();
		}
		catch (SlashCommandParserException ex)
		{
			logger.LogError(ex.Message);
			return new BadRequestResult();
		}
		catch (Exception ex)
		{
			logger.LogError(ex.Message);
			return new BadRequestResult();
		}
	}
}
