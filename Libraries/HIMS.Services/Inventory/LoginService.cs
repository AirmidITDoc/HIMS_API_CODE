﻿using HIMS.Data.Models;
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
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                // Update header table records
                LoginManager user = await _context.LoginManagers.FindAsync(objLogin.UserId);
                user.Password = user.Password;
                user.ModifiedDate = objLogin.ModifiedDate;
                user.ModifiedBy = objLogin.ModifiedBy;
                _context.LoginManagers.Update(user);
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }

        //public virtual async Task UpdatePasswordAsync(LoginManager objLogin, int currentUserId, string currentUserName)
        //{
        //    using var scope = new TransactionScope(TransactionScopeOption.Required,
        //      new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted } TransactionScopeAsyncFlowOption.Enabled);

        //    try
        //    {
        //        LoginManager user = await _context.LoginManagers.FindAsync(objLogin.UserId);

        //        if (user == null)
        //        {

        //        }

        //        user.Password = objLogin.Password; 
        //        user.ModifiedDate = DateTime.Now;
        //        user.ModifiedBy = currentUserId;

        //        _context.LoginManagers.Update(user);
        //        await _context.SaveChangesAsync();

        //        scope.Complete();
        //    }
           
        //}

    }
}