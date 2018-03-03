namespace eBarService.Interfaces
{
    public interface IUserOperations
    {
        bool IsUserValid(string usernameOrEmail, string password);
        string RegisterUser(UserTbl userToRegister);
    }
}