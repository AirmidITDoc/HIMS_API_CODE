using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.Inventory
{
    public class LoginService : ILoginService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public LoginService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<LoginManagerListDto>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LoginManagerListDto>(model, "ps_Rtrv_UserList");
        }
        public virtual async Task<IPagedList<LoginConfigUserWiseListDto>> GetListAsyncL(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LoginConfigUserWiseListDto>(model, "ps_M_LoginConfigListUserWise");
        }
        public virtual async Task<IPagedList<LoginStoreUserWiseListDto>> GetListAsyncLC(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LoginStoreUserWiseListDto>(model, "ps_M_LoginStoreUserWise");
        }
        public virtual async Task<IPagedList<LoginAccessConfigListDto>> GetListAsyncLA(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LoginAccessConfigListDto>(model, "ps_M_LoginAccessConfigList");
        }
        public virtual async Task<IPagedList<LoginUnitUserWiseListDto>> GetListAsyncLU(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<LoginUnitUserWiseListDto>(model, "ps_M_LoginUnitUserWise");
        }





        public virtual async Task InsertAsync(LoginManager objLogin, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.LoginManagers.Add(objLogin);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        public virtual async Task UpdateAsync(LoginManager objLogin, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Delete details table realted records

                var lst = await _context.TLoginAccessDetails.Where(x => x.LoginId == objLogin.UserId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.TLoginAccessDetails.RemoveRange(lst);
                }

                // Delete details table realted records
                var lsts = await _context.TLoginUnitDetails.Where(x => x.LoginId == objLogin.UserId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.TLoginUnitDetails.RemoveRange(lsts);
                }
                // Delete details table realted records
                var lstd = await _context.TLoginStoreDetails.Where(x => x.LoginId == objLogin.UserId).ToListAsync();
                if (lst.Count > 0)
                {
                    _context.TLoginStoreDetails.RemoveRange(lstd);
                }
                // Update header & detail table records
                _context.LoginManagers.Update(objLogin);
                _context.Entry(objLogin).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                scope.Complete();
            }
        }
        public virtual async Task CancelAsync(LoginManager objLogin, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                LoginManager objLog = await _context.LoginManagers.FindAsync(objLogin.UserId);
                objLog.IsActive = false;
                objLog.CreatedDate = objLogin.CreatedDate;
                objLog.CreatedBy = objLogin.CreatedBy;
                _context.LoginManagers.Update(objLog);
                _context.Entry(objLog).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
       
        public virtual async Task updatepassAsync(LoginManager objLogin, int CurrentUserId, string CurrentUserName)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled
            );

            LoginManager user = await _context.LoginManagers.FindAsync(objLogin.UserId);
            if (user != null)
            {
                user.UserName = objLogin.UserName;     
                user.Password = objLogin.Password;
                user.LastPasswordChangedDate = objLogin.LastPasswordChangedDate;
                user.LastLoginDate = objLogin.LastLoginDate;

                user.ModifiedBy = objLogin.ModifiedBy;
                user.ModifiedDate = objLogin.ModifiedDate;

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
        //public virtual async Task updatepassAsync(LoginManager objLogin, int CurrentUserId, string CurrentUserName)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
        //    {
        //        // Update header table records
        //        LoginManager user = await _context.LoginManagers.FindAsync(objLogin.UserId);
        //        user.Password = user.Password;
        //        user.ModifiedDate = objLogin.ModifiedDate;
        //        user.ModifiedBy = objLogin.ModifiedBy;
        //        _context.LoginManagers.Update(user);
        //        _context.Entry(user).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
        //}

    }
}