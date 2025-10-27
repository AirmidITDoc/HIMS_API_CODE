using HIMS.Data.Models;

namespace HIMS.Services.Nursing
{
    public partial interface IIPLabRequestService
    {
        //TDlabRequest objTDlabRequest,
        Task InsertAsyncSP(THlabRequest objTHlabRequest, int currentUserId, string currentUserName);
    }
}
