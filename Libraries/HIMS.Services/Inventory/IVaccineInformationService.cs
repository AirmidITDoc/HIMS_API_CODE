using HIMS.Data.Models;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public interface IVaccineInformationService
    {
        Task InsertAsync(TVaccineInformation objVaccineInfo, int userId, string userName);
        Task UpdateAsync(TVaccineInformation objVaccineInfo, int userId, string userName, string[]? ignoreColumns = null);
    }
}