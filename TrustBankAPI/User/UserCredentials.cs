namespace TrustBankAPI.User
{
    public class UserCredentials
    {
        public static List<UserModel> Users { get; set; } = new List<UserModel>()
        {
            new UserModel
            {
                GivenName = "ghazanfar",
                SurName = "mahmood",
                UserName = "gm-admin",
                Password = "password",
                EmailAddress = "email",
                Role = "Admin"
            },
            new UserModel
            {
                GivenName = "ghazanfar",
                SurName = "mahmood",
                UserName = "gm-user",
                Password = "password",
                EmailAddress = "email",
                Role = "User"
            }
        };
    }
}
