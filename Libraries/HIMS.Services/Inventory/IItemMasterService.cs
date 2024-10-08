﻿using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface IItemMasterService
    {
        Task InsertAsyncSP(MItemMaster objItemMaster, int UserId, string Username);
        Task InsertAsync(MItemMaster objItemMaster, int UserId, string Username);
        Task UpdateAsync(MItemMaster objItemMaster, int UserId, string Username);
        Task CancelAsync(MItemMaster objItemMaster, int UserId, string Username);

    }
}
