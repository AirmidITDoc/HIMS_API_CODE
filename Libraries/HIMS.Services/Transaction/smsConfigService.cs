﻿using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Transaction
{
    public class smsConfigService : IsmsConfigService
    {
        private readonly HIMSDbContext _context;
        public smsConfigService(HIMSDbContext HIMSDbContext)
        {
            _context = HIMSDbContext;
        }

        public virtual async Task<IPagedList<SMSConfigListDto>> GetSMSconfig(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<SMSConfigListDto>(model, "Rtrv_Sent_SMS_List");
        }

      
        public virtual async Task InsertAsyncSP(SsSmsConfig objSsSmsConfig, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { "  " };
            var entity = objSsSmsConfig.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("PS_M_insert_SMS_Config", CommandType.StoredProcedure, entity);

            await _context.SaveChangesAsync(UserId, Username);
        }
        public virtual async Task UpdateAsyncSP(SsSmsConfig objSsSmsConfig, int UserId, string Username)
        {
            DatabaseHelper odal = new();
            string[] rEntity = { " " };
            var entity = objSsSmsConfig.ToDictionary();
            foreach (var rProperty in rEntity)
            {
                entity.Remove(rProperty);
            }

            odal.ExecuteNonQuery("PS_m_Update_SMS_Config", CommandType.StoredProcedure, entity);

            await _context.SaveChangesAsync(UserId, Username);
        }
    }
}
