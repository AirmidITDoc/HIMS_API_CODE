using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Users
{
    public partial interface IUserService
    {
        Task<LoginManager> CheckLogin(string UserName, string Password);
        Task UpdateAsync(LoginManager user, int UserId, string Username);
        bool CheckTokenIsValidAsync(int UserId, string UserToken);
    }
}
