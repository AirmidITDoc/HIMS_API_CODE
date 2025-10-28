namespace HIMS.Data.Models
{
    public partial class MAssignItemToStore
    {
        public long AssignId { get; set; }
        public long StoreId { get; set; }
        public long ItemId { get; set; }

        public virtual MItemMaster Item { get; set; } = null!;
        public virtual MStoreMaster Store { get; set; } = null!;
    }
}
