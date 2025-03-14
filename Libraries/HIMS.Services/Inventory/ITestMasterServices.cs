﻿using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.Inventory
{
    public partial interface ITestMasterServices
    {
        Task<IPagedList<TestMasterListDto>> GetListAsync(GridRequestModel objGrid);
        Task InsertAsync(MPathTestMaster objTest, int UserId, string Username);
        Task InsertAsyncSP(MPathTestMaster objTest, int UserId, string Username);
        Task UpdateAsync(MPathTestMaster objTest, int UserId, string Username);
        //Task CancelAsync(MPathTestMaster objTest, int CurrentUserId, string CurrentUserName);


    }
}
