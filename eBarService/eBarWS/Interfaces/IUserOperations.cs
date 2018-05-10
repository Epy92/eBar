using DBModels;

namespace eBarWS.Interfaces
{
    public interface IUserOperations
    {
        UserTbl GetUser(string usernameOrEmail, string password);
        string RegisterUser(UserTbl userToRegister);
        void GenerateResetCode(string userOrEmail, out string message, out bool responseFlag);
        string ResetUserPassword(string username, string resetCode, string newPassword);
    }
}