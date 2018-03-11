using System.ServiceModel;
using System.ServiceModel.Web;

namespace eBarService.ServiceInterfaces
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "Register")]
        string Register(UserTbl userRegister);

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Bare,    
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "UserLogin")]
        string UserLogin(UserTbl userLogin);

        [OperationContract]
        [WebInvoke(Method = "POST",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GenerateResetCode/{usernameOrEmail}")]
        string GenerateResetCode(string usernameOrEmail);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "ResetUserPassword")]
        string ResetUserPassword(string username, string resetCode, string newPassword);
    }
}
