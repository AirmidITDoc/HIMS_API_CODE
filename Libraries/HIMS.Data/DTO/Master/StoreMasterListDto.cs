using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Master
{
    public class StoreMasterListDto
    {

        public long StoreId { get; set; }
        public string StoreShortName { get; set; }
        public string StoreName { get; set; }
        public string IndentPrefix { get; set; }
        public string IndentNo { get; set; }
        public string PurchasePrefix { get; set; }
        public string PurchaseNo { get; set; }
        public string GrnPrefix { get; set; }
        public string GrnNo { get; set; }
        public string GrnreturnNoPrefix { get; set; }
        public string GrnreturnNo { get; set; }
        public string ReturnFromDeptNoPrefix { get; set; }
        public string ReturnFromDeptNo { get; set; }
        public long AddedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
