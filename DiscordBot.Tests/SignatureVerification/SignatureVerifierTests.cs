using DiscordBot.SignatureVerification;

namespace DiscordBot.Tests.SignatureVerification;

[TestClass]
public class SignatureVerifierTests
{
    private ISignatureVerifier _signatureVerifier = null!;
    private SignatureVerificationRequest _signatureVerificationRequest = null!;
    
    [TestInitialize]
    public void Initialize()
    {
        _signatureVerifier = new SignatureVerifier();
        _signatureVerifier.PerformVerification = 
            (_, _, _, _) => true;
        
        _signatureVerificationRequest = new SignatureVerificationRequest(
            "signature", 
            "timestamp", 
            "body");
    }
    
    [TestMethod]
    public void TestSucceeds()
    {
        Assert.IsTrue(_signatureVerifier.Verify(_signatureVerificationRequest, "a"));
    }

    [TestMethod]
    [ExpectedException(typeof(SignatureException))]
    public void TestFailsWithEmptyPublicKey()
    {
        _signatureVerifier.Verify(_signatureVerificationRequest, "");
    }

    [TestMethod]
    [ExpectedException(typeof(SignatureException))]
    public void TestFailsWithNullPublicKey()
    {
        _signatureVerifier.Verify(_signatureVerificationRequest, null);
    }

    [TestMethod]
    [ExpectedException(typeof(SignatureException))]
    public void TestFailsWithEmptySignature()
    {
        _signatureVerificationRequest.Signature = "";
        _signatureVerifier.Verify(_signatureVerificationRequest, "a");
    }
    
    [TestMethod]
    [ExpectedException(typeof(SignatureException))]
    public void TestFailsWithEmptyTimestamp()
    {
        _signatureVerificationRequest.Timestamp = "";
        _signatureVerifier.Verify(_signatureVerificationRequest, "a");
    }
    
    [TestMethod]
    [ExpectedException(typeof(SignatureException))]
    public void TestFailsWithInvalidSignature()
    {
        _signatureVerifier.PerformVerification = (_, _, _, _) => false; 
        _signatureVerifier.Verify(_signatureVerificationRequest, "a");
    }
}