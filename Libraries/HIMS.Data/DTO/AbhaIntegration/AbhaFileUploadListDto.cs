using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.AbhaIntegration
{
    public class AbhaFileUploadListDto
    {
            public long FileId { get; set; }
            public string? RegNo { get; set; }
            public string? Opipno { get; set; }
            public long? Opipid { get; set; }
            public long? Opiptype { get; set; }
            public string? FileName { get; set; }
            public DateTime? CreatedDate { get; set; }
        
    }
    public class AbhaAddressDropdownDto
    {
        public string? AbhaAddress { get; set; }
        public string? AbhaNumber { get; set; }

    }
}
