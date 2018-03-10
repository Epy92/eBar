namespace eBarService.Interfaces
{
    public interface IUserOperations
    {
        bool IsUserValid(string usernameOrEmail, string password);
        string RegisterUser(UserTbl userToRegister);
        void GenerateResetCode(string userOrEmail, out string message, out bool responseFlag);
        string ResetUserPassword(string username, string resetCode, string newPassword);
    }
}