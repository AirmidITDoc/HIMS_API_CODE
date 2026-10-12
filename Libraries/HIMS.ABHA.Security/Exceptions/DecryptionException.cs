namespace HIMS.ABHA.Security.Exceptions;

public class DecryptionException : FideliusCryptoException
{
    public DecryptionException(string message) : base(message) { }
    public DecryptionException(string message, Exception innerException) : base(message, innerException) { }
}