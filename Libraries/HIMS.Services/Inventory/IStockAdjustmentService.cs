﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIMS.Data.Models;

namespace HIMS.Services.Inventory
{
    public partial interface IStockAdjustmentService
    {
        Task UpdateAsync(TIssueToDepartmentDetail objStock, int UserId, string Username);
        Task InsertAsync(TIssueToDepartmentDetail objStock, int UserId, string Username);
        Task InsertAsync(TStockAdjustment objStock, int UserId, string Username);
        Task InsertAsync(TBatchAdjustment objStock, int UserId, string Username);
        Task InsertAsync(TMrpAdjustment objStock, int UserId, string Username);
    }
}