using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.DietKitchen;
using HIMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.DietKitchen
{
    public class DietAssessmentInfoService : IDietAssessmentInfoService
    {
        private readonly HIMSDbContext _context;

        public DietAssessmentInfoService(HIMSDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IPagedList<DietAssessmentInfoDtoList>> GetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<DietAssessmentInfoDtoList>(model, "ps_Rtrv_DietAssessmentInfo");
        }

        public virtual async Task<IPagedList<PatientvitalsdetailListDto>> DietGetListAsync(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<PatientvitalsdetailListDto>(model, "ps_Patient_vitalsdetail");
        }

    }
}