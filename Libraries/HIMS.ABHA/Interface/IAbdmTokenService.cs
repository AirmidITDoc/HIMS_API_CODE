namespace HIMS.ABHA.Interface
{
    public interface IAbdmTokenService
    {
        Task<string> GetAccessTokenAsync(bool forceRefresh = false);
        Task<string> GetPublicCertificateAsync();
    }
}
