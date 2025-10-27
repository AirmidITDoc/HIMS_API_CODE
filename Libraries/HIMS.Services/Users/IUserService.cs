using HIMS.Data.Models;

namespace HIMS.Services.Users
{
    public partial interface IUserService
    {
        Task<LoginManager> CheckLogin(string UserName, string Password);
        Task UpdateAsync(LoginManager user, int UserId, string Username);
        bool CheckTokenIsValidAsync(int UserId, string UserToken, string LoginType);
        Task<LoginManager> GetById(int Id);
    }
}
