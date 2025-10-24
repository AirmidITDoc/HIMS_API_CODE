using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HIMS.Services.Nursing
{
    public partial interface INursingConsentService
    {

        Task<IPagedList<ConsentDeptListDto>> GetListAsync(GridRequestModel objGrid);
        void Insert(TConsentInformation ObjTConsentInformation, int UserId, string Username);
        void Update(TConsentInformation ObjTConsentInformation, int UserId, string Username);
        Task<IPagedList<ConsentpatientInfoListDto>> ConsentpatientInfoList(GridRequestModel objGrid);
        Task<List<MConsentMaster>> GetConsent(int DeptId);





    }
}
