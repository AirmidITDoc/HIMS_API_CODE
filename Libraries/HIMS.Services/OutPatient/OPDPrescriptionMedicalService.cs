﻿using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.OutPatient
{
    public class OPDPrescriptionMedicalService : IOPDPrescriptionMedicalService
    {
        private readonly Data.Models.HIMSDbContext _context;
        public OPDPrescriptionMedicalService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }


        //Ashu///
        public virtual async Task InsertPrescriptionAsyncSP(TPrescription objTPrescription, List<TOprequestList> objTOprequestList, List<MOpcasepaperDignosisMaster> objmOpcasepaperDignosisMaster, int UserId, string UserName)
        {

            DatabaseHelper odal = new();
            var tokensObj = new
            {
                OPIPID = Convert.ToInt32(objTPrescription.OpdIpdIp)
            };
            odal.ExecuteNonQuery("sp_delete_OPPrescription_1", CommandType.StoredProcedure, tokensObj.ToDictionary());

            string[] rEntity = { "PrecriptionId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" };
            var entity = objTPrescription.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }
            entity["IsAddBy"] = 0; // Ensure objpayment has OPDIPDType
            odal.ExecuteNonQuery("ps_insert_OPPrescription_1", CommandType.StoredProcedure, entity);

            foreach (var item in objTOprequestList)
            {

                string[] rDetailEntity = { "RequestTranId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn" };

                var PrescriptionEntity = item.ToDictionary();
                foreach (var rProperty in rDetailEntity)
                {
                    PrescriptionEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("sp_Insert_T_OPRequestList", CommandType.StoredProcedure, PrescriptionEntity);

            }

            foreach (var item in objmOpcasepaperDignosisMaster)
            {
                string[] PayEntity = { "Id", };
                var CasepaperEntity = item.ToDictionary();
                foreach (var rProperty in PayEntity)
                {
                    CasepaperEntity.Remove(rProperty);
                }
                odal.ExecuteNonQuery("sp_Insert_OPCasepaperDignosisMaster", CommandType.StoredProcedure, CasepaperEntity);


            }
        }
    }
}
