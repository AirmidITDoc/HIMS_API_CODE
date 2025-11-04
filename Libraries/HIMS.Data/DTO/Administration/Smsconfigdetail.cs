using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class Smsconfigdetail
    {

       public string? url { get; set; }
        public string? keys { get; set; }

        public string? campaign { get; set; }
        //public long? routeid { get; set; }
        public string? SenderId { get; set; }

        public string? UserName { get; set; }
        public string? SPassword { get; set; }
        public string? StorageLocLink { get; set; }

        public string? ConType { get; set; }
       
    }
}
