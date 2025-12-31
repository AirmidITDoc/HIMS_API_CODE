using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HIMS.Services.Users
{
    public class UserService : IUserService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public UserService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<LoginManager> CheckLogin(string UserName, string Password)
        {
            return await _context.LoginManagers.FirstOrDefaultAsync(x => x.UserName.ToLower() == UserName.ToLower() && x.Password == Password && x.IsActive == true);
        }
        //public virtual async Task<LoginManager> CheckLogin(string UserName, string Password)
        //{
        //    var objUser= await _context.LoginManagers.FirstOrDefaultAsync(x => x.UserName.ToLower() == UserName.ToLower() && x.Password == Password && x.IsActive == true);
        //    objUser.TLoginStoreDetails = await  _context.TLoginStoreDetails.Where(x=>x.LoginId == objUser.UserId).ToListAsync();
        //    return objUser;
        //}
        public virtual async Task<LoginManager> GetById(int Id)
        {
            return await _context.LoginManagers.FirstOrDefaultAsync(x => x.UserId == Id);
        }
        public virtual async Task UpdateAsync(LoginManager user, int UserId, string Username)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public virtual bool CheckTokenIsValidAsync(int UserId, string UserToken, string LoginType)
        {
            var query = from u in _context.LoginManagers
                        where (LoginType == "Mobile" ? u.MobileToken.ToLower() : u.UserToken.ToLower()) == UserToken.ToLower() && u.UserId == UserId
                        select u;

            return query.Any();
        }
    }
}
