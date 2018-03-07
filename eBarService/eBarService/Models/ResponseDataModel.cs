using System.Runtime.Serialization;

namespace eBarService.Models
{
    [DataContract]
    public class ResponseDataModel
    {
        [DataMember]
        public bool ResultFlag { get; set; }
        [DataMember]
        public ResultCode ResultCode { get; set; }
        [DataMember]
        public string ResultMessage { get; set; }
    }

    public enum ResultCode
    {
        InvalidOperation = 1,
        NotAuthorized,
        OperationFailed,
        OperationSuccess,
        UnexpectedError,
        UserInvalid
    }
}