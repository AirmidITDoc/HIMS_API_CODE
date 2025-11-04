using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class AuditlogDtoList
    {
        public int? Id { get; set; }
        public int? ActionId { get; set; }
        public long? ActionById { get; set; }
        
        public string? ActionByName { get; set; }
        //public long? EntityId { get; set; }
        public string? EntityName { get; set; }

        public string? Description { get; set; }
        public string? AdditionalInfo { get; set; }
        public int? LogTypeId { get; set; }

        public int? LogSourceId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
