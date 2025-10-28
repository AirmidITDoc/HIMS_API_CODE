using HIMS.Data.Models;

namespace HIMS.Services.Masters
{
    public partial interface ICampMasterService
    {
        Task<MCampMaster> GetById(int Id);
        //Task<IPagedList<CampMasterListDto>> MPathParameterList(GridRequestModel objGrid);
        Task InsertAsync(MCampMaster objMCampMaster, int UserId, string Username);
        Task UpdateAsync(MCampMaster objMCampMaster, int UserId, string Username);
        Task CancelAsync(MCampMaster objMCampMaster, int UserId, string Username);
        //Task UpdateFormulaAsync(MCampMaster objMCampMaster, int UserId, string Username);
    }
}
