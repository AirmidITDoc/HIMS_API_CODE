using HIMS.ABHA.Models.M2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.ABHA.Interface
{
    public interface IFhirPrescriptionService
    {
        Task<Binary> CreatePrescriptionBundle(string registrationId);
    }
}
