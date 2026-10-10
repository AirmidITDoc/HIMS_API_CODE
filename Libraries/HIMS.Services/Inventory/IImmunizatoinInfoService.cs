using HIMS.Data.Models;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public interface IImmunizatoinInfoService
    {
        
        Task<TImmunizatoinInfo?> GetByIdAsync(long id);
        Task ImmunizationInfoAsync(
       List<TImmunizatoinInfo> models,
       long regId,
       int UserId,
       string Username);
    }
}