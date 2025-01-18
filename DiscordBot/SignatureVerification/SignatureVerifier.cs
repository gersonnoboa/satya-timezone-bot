using System.Text;
using NSec.Cryptography;

namespace DiscordBot.SignatureVerification;

internal interface ISignatureVerifier
{
	Func<string, string, string, string, bool> PerformVerification { get; set; }
	bool Verify(SignatureVerificationRequest request, string? publicKey);
}

internal class SignatureException(string message) : Exception(message) { }

internal class SignatureVerifier: ISignatureVerifier
{
	public Func<string, string, string, string, bool> PerformVerification { get; set; } = 
		(signatureString, timestampString, bodyString, publicKeyString) =>
	{
		var signatureBytes = Convert.FromHexString(signatureString);
		var messageBytes = Encoding.UTF8.GetBytes(timestampString + bodyString);
		
		var algorithm = SignatureAlgorithm.Ed25519;
		var publicKeyBytes = Convert.FromHexString(publicKeyString);
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

		var isVerified = PerformVerification(signature, timestamp, request.Body, publicKey);

		if (!isVerified)
		{
			throw new SignatureException("Invalid signature");
		}

		return isVerified;
	}
}
