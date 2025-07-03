using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface IDoctorMasterService
    {
        Task InsertAsync(DoctorMaster objDoctorMaster, int UserId, string Username);
        Task InsertAsyncSP(DoctorMaster objDoctorMaster, int UserId, string Username);
        Task UpdateAsync(DoctorMaster objDoctorMaster, int UserId, string Username);
        Task<IPagedList<DoctorMasterListDto>> GetListAsync(GridRequestModel objGrid);
        Task<IPagedList<DoctorChargesDetailListDto>> ListAsync(GridRequestModel objGrid);
        Task<IPagedList<DoctorSignpageListDto>> ListAsyncs(GridRequestModel objGrid);
        Task<IPagedList<DoctorQualificationDetailsListDto>> ListAsyncQ(GridRequestModel objGrid);
        Task<IPagedList<DoctorLeaveDetailsListDto>> ListAsyncL(GridRequestModel objGrid);
        Task<DoctorMaster> GetById(int Id);
        Task<IPagedList<DoctorMaster>> GetAllPagedAsync(GridRequestModel objGrid);
        Task<IPagedList<LvwDoctorMasterList>> GetListAsync1(GridRequestModel model);
        Task<List<DoctorMaster>> GetDoctorsByDepartment(int DeptId);
        Task<List<DoctorMaster>> SearchDoctor(string str);
        Task<IPagedList<DoctorShareListDto>> GetList(GridRequestModel objGrid);
        Task<IPagedList<DoctorShareLbyNameListDto>> GetList1(GridRequestModel objGrid);
        Task<List<DoctorMaster>> GetDoctorWithDepartment();
        Task<List<ContantListDto>> ConstantListAsync(string ConstantType);



    }
}
