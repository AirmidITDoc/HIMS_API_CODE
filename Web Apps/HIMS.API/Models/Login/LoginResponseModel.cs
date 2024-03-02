namespace HIMS.API.Models
{
    public class LoginResponseModel : APIResponseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Token { get; set; }
        public string Issued { get; set; }
        public string Expires { get; set; }
    }
}
