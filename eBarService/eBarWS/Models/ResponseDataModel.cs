using System.Runtime.Serialization;

namespace eBarWS.Models
{
    [DataContract]
    public class ResponseDataModel
    {
        public bool ResultFlag { get; set; }
        public string ResultCode { get; set; }
        public string ResultMessage { get; set; }
    }

    public enum ResultCode
    {
        InvalidOperation = 1,
        RestaurantSaved,
        NotAuthorized,
        OperationFailed,
        DuplicateUser,
        OperationSuccess,
        UnexpectedError,
        UserInvalid,
        RegisterSuccess,
        LoginSuccess,
        MissingUser,
        ResetCodeGenerated,
        ResetCodeAlreadyGenerated,
        GenerateResetCodeFailed,
        PasswordChanged
    }
}