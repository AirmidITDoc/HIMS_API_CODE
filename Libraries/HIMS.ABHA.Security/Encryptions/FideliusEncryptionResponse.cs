namespace HIMS.ABHA.Security.Encryptions;

public record FideliusEncryptionResponse(
    string EncryptedData,
    string Iv,
    string Salt);
