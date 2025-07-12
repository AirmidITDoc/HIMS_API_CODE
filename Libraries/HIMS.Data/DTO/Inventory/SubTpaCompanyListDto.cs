using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class SubTpaCompanyListDto
    {
        public long SubCompanyId { get; set; }
        public long? CompanyId { get; set; }
        public long? CompTypeId { get; set; } 
        public string? CompanyName { get; set; }
        public string? CompanyShortName { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public string? PhoneNo { get; set; }
        public string? FaxNo { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CityName { get; set; }
        public string? StateName { get; set; }
        public string? CountryName { get; set; }


    }
}
