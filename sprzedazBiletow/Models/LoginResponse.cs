
namespace sprzedazBiletow.Models
{
    public class LoginResponse
    {
        public LoginResponse(bool Status, int UserID, string FirstName, string LastName, string Email, string Login)
        {
            this.Status = Status;
            this.UserID = UserID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Login = Login;
        }

        public bool Status { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
    }
}