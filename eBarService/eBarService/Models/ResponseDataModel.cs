using System.Runtime.Serialization;

namespace eBarService.Models
{
    [DataContract]
    public class ResponseDataModel
    {
        [DataMember]
        public bool ResultFlag { get; set; }
        [DataMember]
        public string ResultCode { get; set; }
        [DataMember]
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
        GenerateResetCodeFailed,
        PasswordChanged
    }
}