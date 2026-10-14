using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.DietKitchen;
using HIMS.Data.Models;
using System.Threading.Tasks;

namespace HIMS.Services.DietKitchen
{
    public interface IDietAssessmentInfoService
    {
        
        Task<IPagedList<DietAssessmentInfoDtoList>> GetListAsync(GridRequestModel model);
       
    }
}