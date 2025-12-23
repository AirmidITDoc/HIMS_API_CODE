using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.OTManagment
{
    public class OTOperativeNotesService : IOTOperativeNotes
    {
        private readonly HIMSDbContext _context;
        public OTOperativeNotesService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task InsertAsync(TOtOperativeNote ObjTOtOperativeNotes, int UserId, string Username)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }, TransactionScopeAsyncFlowOption.Enabled);
            {
                _context.TOtOperativeNotes.Add(ObjTOtOperativeNotes);
                await _context.SaveChangesAsync();

                scope.Complete();
            }
        }
    }
}
