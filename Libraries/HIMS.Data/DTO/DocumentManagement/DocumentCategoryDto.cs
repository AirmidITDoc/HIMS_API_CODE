using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.DocumentManagement
{
    public class DocumentCategoryDto
    {
        public long Id { get; set; }

        public long? ParentId { get; set; }

        public string DocCategory { get; set; }

        public string? Icon { get; set; }

        public int DocumentCount { get; set; }

        public List<DocumentCategoryDto> Children { get; set; } = new();
    }
}
