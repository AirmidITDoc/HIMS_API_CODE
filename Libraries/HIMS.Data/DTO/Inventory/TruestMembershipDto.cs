using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class TruestMembershipDto
    {
        public long MembershipId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Aadhaar { get; set; }
        public string? Mobile { get; set; }
        public long PrefixId { get; set; }
        public long GenderId { get; set; }
        public DateTime Dob { get; set; }
        public long AgeY { get; set; }
        public long AgeM { get; set; }
        public long AgeD { get; set; }
        public string? PAN { get; set; }
        public string? Email { get; set; }
        public long CityId { get; set; }
        public string? ResidenceAddress { get; set; }
        public string? MemberType { get; set; }
        public string? PatientName { get; set; }

    }
}

