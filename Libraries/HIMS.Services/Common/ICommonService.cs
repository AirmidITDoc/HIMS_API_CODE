using HIMS.Core.Domain.Grid;

namespace HIMS.Services.Common
{
    public partial interface ICommonService
    {
        dynamic GetDDLByIdWithProc(DDLRequestModel model);
        dynamic GetDataSetByProc(string sp_Name, List<SearchGrid> SearchFields);
        List<T> GetSingleListByProc<T>(ListRequestModel model);
        dynamic GetDataTableByProc(string sp_Name, List<SearchGrid> SearchFields);
    }
}
