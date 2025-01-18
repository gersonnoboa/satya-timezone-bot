using System.Text;
using DiscordBot.InteractionRunner;
using NSec.Cryptography;

namespace DiscordBot.SignatureVerification;

internal interface ISignatureVerifier
{
	Func<byte[], byte[], byte[], bool> PerformVerification { get; set; }
	bool Verify(SignatureVerificationRequest request, string? publicKey);
}

internal class SignatureException(string message) : Exception(message) { }

internal class SignatureVerifier: ISignatureVerifier
{
	public Func<byte[], byte[], byte[], bool> PerformVerification { get; set; } = (publicKeyBytes, messageBytes, signatureBytes) =>
	{
		var algorithm = SignatureAlgorithm.Ed25519;
		var publicKey = PublicKey.Import(algorithm, publicKeyBytes, KeyBlobFormat.RawPublicKey);

		return algorithm.Verify(publicKey, messageBytes, signatureBytes);
	};

	public bool Verify(SignatureVerificationRequest request, string? publicKey)
	{
		if (string.IsNullOrEmpty(publicKey))
		{
			throw new SignatureException("No public key found");
		}

		var signature = request.Signature;
		var timestamp = request.Timestamp;

		if (string.IsNullOrEmpty(signature) || string.IsNullOrEmpty(timestamp))
		{
			throw new SignatureException("No signature or timestamp provided");
		}

		var isVerified = VerifySignature(signature, timestamp, request.Body, publicKey);

		if (!isVerified)
		{
			throw new SignatureException("Invalid signature");
		}

		return isVerified;
	}

	private bool VerifySignature(string signature, string timestamp, string body, string publicKey)
	{
		var publicKeyBytes = Convert.FromHexString(publicKey);
		var messageBytes = Encoding.UTF8.GetBytes(timestamp + body);
		var signatureBytes = Convert.FromHexString(signature);
		
		return PerformVerification(publicKeyBytes, messageBytes, signatureBytes);
	}
}
