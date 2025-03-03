using Aspose.Cells.Drawing;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Transactions;

namespace HIMS.Services.OutPatient
{
    public class IPAdvanceService : IIPAdvanceService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public IPAdvanceService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

       

    
        
    }
}
