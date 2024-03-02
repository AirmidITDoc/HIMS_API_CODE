using HIMS.Core.Domain.Logging;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Users
{
    public class UserService : IUserService
    {
        private readonly Data.Models.SSDB_JSS_WEB_25SepContext _context;
        public UserService(SSDB_JSS_WEB_25SepContext sSDB_JSS_WEB_25SepContext)
        {
            _context = sSDB_JSS_WEB_25SepContext;
        }
        public virtual async Task<LoginManager> CheckLogin(string UserName, string Password)
        {
            return await _context.LoginManagers.FirstOrDefaultAsync(x => x.UserName.ToLower() == UserName.ToLower() && x.Password == Password && x.IsActive == true);
        }
        public virtual async Task UpdateAsync(LoginManager user, int UserId, string Username)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public virtual bool CheckTokenIsValidAsync(int UserId, string UserToken)
        {
            var query = from u in _context.LoginManagers
                        where u.UserToken.ToLower() == UserToken.ToLower() && u.UserId == UserId
                        select u;

            return query.Any();
        }
    }
}
