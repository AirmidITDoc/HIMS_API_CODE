﻿using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MStoreMaster
    {
        public MStoreMaster()
        {
            MAssignItemToStores = new HashSet<MAssignItemToStore>();
            MAssignSupplierToStores = new HashSet<MAssignSupplierToStore>();
        }

        public long StoreId { get; set; }
        public string? StoreShortName { get; set; }
        public string? StoreName { get; set; }
        public string? IndentPrefix { get; set; }
        public string? IndentNo { get; set; }
        public string? PurchasePrefix { get; set; }
        public string? PurchaseNo { get; set; }
        public string? GrnPrefix { get; set; }
        public string? GrnNo { get; set; }
        public string? GrnreturnNoPrefix { get; set; }
        public string? GrnreturnNo { get; set; }
        public string? IssueToDeptPrefix { get; set; }
        public string? IssueToDeptNo { get; set; }
        public string? ReturnFromDeptNoPrefix { get; set; }
        public string? ReturnFromDeptNo { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsActive { get; set; }
        public long? UpdatedBy { get; set; }
        public string? WorkOrderPrefix { get; set; }
        public string? WorkOrderNo { get; set; }
        public long? PharSalCountId { get; set; }
        public long? PharSalRecCountId { get; set; }
        public long? PharSalReturnCountId { get; set; }
        public long? PharAdvId { get; set; }
        public long? PharAdvReptId { get; set; }
        public long? PharAdvRefId { get; set; }
        public long? PharAdvRefReptId { get; set; }
        public string? PrintStoreName { get; set; }
        public string? DlNo { get; set; }
        public string? Gstin { get; set; }
        public string? StoreAddress { get; set; }
        public string? HospitalMobileNo { get; set; }
        public string? HospitalEmailId { get; set; }
        public string? PrintStoreUnitName { get; set; }
        public bool? IsPharStore { get; set; }
        public bool? IsWhatsAppMsg { get; set; }
        public string? WhatsAppTemplateId { get; set; }
        public bool? IsSmsmsg { get; set; }
        public string? SmstemplateId { get; set; }
        public string? Header { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? TermsAndCondition { get; set; }

        public virtual ICollection<MAssignItemToStore> MAssignItemToStores { get; set; }
        public virtual ICollection<MAssignSupplierToStore> MAssignSupplierToStores { get; set; }
    }
}
