namespace HIMS.ABHA.Security.KeyPairGen;

public record FideliusKeyMaterial(string PrivateKey, string PublicKey, string X509PublicKey, string Nonce);
