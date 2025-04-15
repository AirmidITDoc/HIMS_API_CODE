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