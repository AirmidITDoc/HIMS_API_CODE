using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.MRD;
using HIMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Services.MRD
{
    public partial interface IMRDFileService
    {
        Task InsertAsync(TMrdfileReceived ObjTMrdfileReceived, int UserId, string Username);
        Task UpdateAsync(TMrdfileReceived ObjTMrdfileReceived, int UserId, string Username, string[]? references);
        Task<IPagedList<MRDFileReceivedListDto>> GetListAsync(GridRequestModel objGrid);
        Task InsertOutFileAsync(TMrdoutInFile ObjmrdoutInFile, int UserId, string Username);
        Task InsertInFileAsync(TMrdoutInFile ObjmrdoutInFile, int UserId, string Username);

    }
}
