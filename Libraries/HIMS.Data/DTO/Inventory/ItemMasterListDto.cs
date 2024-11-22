using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public class ItemMasterListDto
    {
        public long ItemID { get; set; }
        public string ItemShortName { get; set; }
        public string ItemName { get; set; }
        public string ItemTypeName { get; set; }
        public long ItemTypeID { get; set; }
        public long ItemCategaryId { get; set; }
        public string ItemCategoryName { get; set; }
        public long ItemClassId { get; set; }
        public string ItemClassName { get; set; }
        public long ItemGenericNameId { get; set; }
        public string ItemGenericName { get; set; }
        public long PurchaseUOMId { get; set; }
        public string PuchaseUOM { get; set; }
        public long StockUOMId { get; set; }
        public string StockUOM { get; set; }
        public string ConversionFactor { get; set; }
        public long CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public double TaxPer { get; set; }
        public bool Isactive { get; set; }
        public long Addedby { get; set; }
        public bool IsBatchRequired { get; set; }
        public float MinQty { get; set; }
        public float MaxQty { get; set; }
        public float ReOrder { get; set; }
        public string StoreName { get; set; }
        public long StoreId { get; set; }
        public string HSNcode { get; set; }
        public double CGST { get; set; }
        public double SGST { get; set; }
        public double IGST { get; set; }
        public long ManufId { get; set; }
        public bool IsNarcotic { get; set; }
        public string ProdLocation { get; set; }
        public bool IsH1Drug { get; set; }
        public bool IsScheduleH { get; set; }
        public bool IsHighRisk { get; set; }
        public bool IsScheduleX { get; set; }
        public bool IsLASA { get; set; }
        public bool IsEmgerency { get; set; }
        public long DrugType { get; set; }
        public string DrugTypeName { get; set; }
        public long ItemCompnayId { get; set; }




    }
}
