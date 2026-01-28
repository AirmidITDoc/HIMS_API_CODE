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
        //public virtual async Task<LoginManager> CheckLogin(string UserName, string Password)
        //{
        //    return await _context.LoginManagers.FirstOrDefaultAsync(x => x.UserName.ToLower() == UserName.ToLower() && x.Password == Password && x.IsActive == true);
        //}
        public virtual async Task<LoginManager> CheckLogin(string UserName, string Password)
        {
            var objUser = await _context.LoginManagers.FirstOrDefaultAsync(x => x.UserName.ToLower() == UserName.ToLower() && x.Password == Password && x.IsActive == true);
            var qry = from d in _context.TLoginStoreDetails
                      join s in _context.MStoreMasters on d.StoreId equals s.StoreId
                      where d.LoginId == objUser.UserId
                      select new TLoginStoreDetail() { LoginId = d.LoginId, StoreId = d.StoreId, StoreName = s.StoreName, LoginStoreDetId = d.LoginStoreDetId };
            objUser.TLoginStoreDetails = await qry.ToListAsync();

            var qry1 = from d in _context.TLoginUnitDetails
                       join s in _context.HospitalMasters on d.UnitId equals s.HospitalId
                       where d.LoginId == objUser.UserId
                       select new TLoginUnitDetail() { LoginId = d.LoginId, UnitId = d.UnitId, UnitName = s.HospitalName, LoginUnitDetId = d.LoginUnitDetId };
            objUser.TLoginUnitDetails = await qry1.ToListAsync();

            //var qry2 = from d in _context.TLoginAccessDetails
            //           where d.LoginId == objUser.UserId
            //           select new TLoginAccessDetail() { LoginId = d.LoginId , AccessValueId = d.AccessValueId, AccessInputValue =d.AccessInputValue, AccessValue=d.AccessValue};
            //objUser.TLoginAccessDetails = await qry2.ToListAsync();

            //objUser.TLoginStoreDetails = await _context.TLoginStoreDetails.Where(x => x.LoginId == objUser.UserId).ToListAsync();
            //objUser.TLoginUnitDetails = await _context.TLoginUnitDetails.Where(x => x.LoginId == objUser.UserId).ToListAsync();

            return objUser;
        }
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
