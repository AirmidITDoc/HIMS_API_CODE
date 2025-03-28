﻿using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Pathlogy
{
    public class  MPathParaRangeWithAgeMasterService : IMPathParaRangeWithAgeMasterService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public MPathParaRangeWithAgeMasterService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task<IPagedList<MPathParaRangeWithAgeMasterListDto>> MPathParaRangeWithAgeMasterList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MPathParaRangeWithAgeMasterListDto>(model, "m_Rtrv_PathParameterRangeWithAge");
        }

    }
}
