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
        Task InsertAsync(TMedicolegalCertificate ObjTMedicolegalCertificate, TMlcinformation ObjTMlcinformation , int UserId, string Username);
        Task UpdateAsync(TMedicolegalCertificate ObjTMedicolegalCertificate, TMlcinformation ObjTMlcinformation, int UserId, string Username);

    }
}
