namespace HIMS.Data.Models
{
    public partial class TFavouriteUserList
    {
        public long FavouriteId { get; set; }
        public long? UserId { get; set; }
        public long? MenuId { get; set; }
    }
}
