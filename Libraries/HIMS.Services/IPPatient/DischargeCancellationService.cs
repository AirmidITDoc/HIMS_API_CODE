using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HIMS.Services.IPPatient
{
    public class DischargeCancellationService : IDischargeCancellationService
    {
        private readonly HIMSDbContext _context;
        public DischargeCancellationService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }
        public virtual async Task UpdateAsync(Admission objAdmission, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "RegId", "AdmissionDate", "AdmissionTime", "PatientTypeId", "HospitalId", "DocNameId", "RefDocNameId", "DischargeDate", "DischargeTime", "IsDischarged", "IsBillGenerated", "Ipdno", "IsCancelled", "CompanyId", "TariffId", "ClassId", "DepartmentId", "RelativeName", "RelativeAddress", "MobileNo", "RelationshipId", "AddedBy", "IsMlc", "MotherName", "AdmittedDoctor1", "AdmittedDoctor2", "IsProcessing", "Ischarity", "RefByTypeId", "RefByName", "IsMarkForDisNur", "IsMarkForDisNurId", "IsMarkForDisNurDateTime", "IsCovidFlag", "IsCovidUserId", "IsCovidUpdateDate", "IsUpdatedBy", "SubTpaComId", "PolicyNo", "AprovAmount", "CompDod", "IsPharClearance", "Ipnumber", "EstimatedAmount","ApprovedAmount","HosApreAmt", "PathApreAmt", "PharApreAmt", "RadiApreAmt","PharDisc","CompBillNo", "CompBillDate","CompDiscount","CompDisDate","CBillNo", "CFinalBillAmt", "CDisallowedAmt", "ClaimNo", "HdiscAmt", "COutsideInvestAmt","RecoveredByPatient", "HChargeAmt", "HAdvAmt","HBillId", "HBillDate", "HBillNo","HTotalAmt", "HDiscAmt1", "HNetAmt", "HPaidAmt","HBalAmt","IsOpToIpconv", "RefDoctorDept","AdmissionType", "MedicalApreAmt", "DischargeSummaries", "Discharges", "TIpPrescriptionDischarges", "PhoneNo", "WardId", "BedId" };
            var entity = objAdmission.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("IP_DISCHARGE_CANCELLATION", CommandType.StoredProcedure, entity);

            await _context.SaveChangesAsync(UserId, Username);
        }
    }
}
