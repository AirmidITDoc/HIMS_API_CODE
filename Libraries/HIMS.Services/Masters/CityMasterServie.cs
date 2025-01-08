using HIMS.Core.Domain.Grid;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Masters
{
    public class   CityMasterServie : ICityMasterServie
    {
        private readonly Data.Models.HIMSDbContext _context;
        public CityMasterServie(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        //public virtual async Task<IPagedList<MCityMaster>> GetAllPagedAsync(GridRequestModel objGrid)
        //{
        //    var qry = from p in _context.MCityMasters
        //              join s in _context.MStateMasters on p.StateId equals s.StateId
        //              select new MCityMaster() { CityId = p.CityId, CityName = p.CityName, IsActive = p.IsActive, StateName = s.StateName, StateId = p.StateId };
        //    return await qry.BuildPredicate(objGrid);
        //}


    }
}
