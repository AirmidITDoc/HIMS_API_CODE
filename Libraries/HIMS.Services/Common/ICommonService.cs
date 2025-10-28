using HIMS.Core.Domain.Grid;

namespace HIMS.Services.Common
{
    public partial interface ICommonService
    {
        dynamic GetDDLByIdWithProc(DDLRequestModel model);
        dynamic GetDataSetByProc(ListRequestModel model);
        List<T> GetSingleListByProc<T>(ListRequestModel model);
    }
}
