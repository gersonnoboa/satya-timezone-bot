using System.Text;
using Microsoft.AspNetCore.Http;
using NSec.Cryptography;

namespace DiscordBot.InteractionRunner;

public class SignatureException(string message) : Exception(message) { }

class SignatureVerifier
{
	public static void Verify(HttpRequest request, string requestBody)
	{
		var publicKey = Environment.GetEnvironmentVariable("DISCORD_PUBLIC_KEY");
		if (string.IsNullOrEmpty(publicKey))
		{
			throw new SignatureException("No public key found");
		}

		// Read required headers
		var signature = request.Headers["x-signature-ed25519"].ToString();
		var timestamp = request.Headers["x-signature-timestamp"].ToString();

		// Ensure headers are present
		if (string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(timestamp))
		{
			throw new SignatureException("No signature or timestamp provided");
		}

		var isVerified = VerifySignature(signature, timestamp, requestBody, publicKey);

		if (!isVerified)
		{
			throw new SignatureException("Invalid signature");
		}
	}

	private static bool VerifySignature(string signature, string timestamp, string body, string publicKey)
	{
		var messageBytes = Encoding.UTF8.GetBytes(timestamp + body);
		var signatureBytes = Convert.FromHexString(signature);

		var algorithm = SignatureAlgorithm.Ed25519;
		var publicKeyBytes = Convert.FromHexString(publicKey);
		var publicKeyEd25519 = PublicKey.Import(algorithm, publicKeyBytes, KeyBlobFormat.RawPublicKey);

		return algorithm.Verify(publicKeyEd25519, messageBytes, signatureBytes);
	}
}
