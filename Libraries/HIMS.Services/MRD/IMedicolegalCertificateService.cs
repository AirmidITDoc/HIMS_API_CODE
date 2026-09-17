using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.MRD
{
    public partial interface IMedicolegalCertificateService
    {
        Task InsertAsync(TMedicolegalCertificate ObjTMedicolegalCertificate, int UserId, string Username);

    }
}
