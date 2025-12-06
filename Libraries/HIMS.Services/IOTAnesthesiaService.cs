using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services
{
    public partial interface IOTAnesthesiaService
    {
        Task InsertAsync(TOtAnesthesiaRecord ObjTOtAnesthesiaRecord, int UserId, string Username);
        Task UpdateAsync(TOtAnesthesiaRecord ObjTOtAnesthesiaRecord, int UserId, string Username, string[]? references);
        Task<List<TOtAnesthesiaPreOpdiagnosisDto>> OtAnesthesiaPreOpdiagnosisListAsync(string descriptionType);



    }
}
