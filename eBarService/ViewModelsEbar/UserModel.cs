namespace ViewModels
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
        public string UserPassword { get; set; }
        public int UserTypeId { get; set; }
        public string UserPreferredLanguage { get; set; }
        public bool? HasRestaurantConfigured { get; set; }
    }

    public enum UserTypesName
    {
        RegularUser = 1,
        RestaurantAdministrator,
        RestaurantEmployee
    }
}
