using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.MRD
{
    public class MRDService :I_MRDCertificate
    {
        private readonly HIMSDbContext _context;
        public MRDService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<MRDCertificateListToDo>> MRDList(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MRDCertificateListToDo>(model, "ps_Retrieve_CharityPatientList");
        }

       
    }
}
