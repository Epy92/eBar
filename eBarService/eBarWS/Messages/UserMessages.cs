namespace eBarWS.Messages
{
    public class UserMessages
    {
        public const string OkMessage = "OK";
        public const string DuplicateUser = "There is already a user registered with the same username or email!";
        public const string UserRegisteredSuccesfully = "User was succesfully registered!";
        public const string ResetCodeGenerated = "An email with the reset code was sent";
        public const string MissingUser = "User does not exist";
        public const string LoginSuccess = "Welcome!";
        public const string GenerateResetCodeFailed = "There was an error while generating the reset code";
        public const string ResetCodeAlreadyGenerated = "There was an error while generating the reset code";
        public const string ResetCodeInvalid = "Reset code does not exist or expired";
        public const string PasswordChanged = "User passoword was changed successfully";
        public const string UnexpectedError = "An unexpected error has occured. Please try again!";
    }
}