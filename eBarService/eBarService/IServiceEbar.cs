using System.ServiceModel;
using System.ServiceModel.Web;

namespace eBarService
{
    [ServiceContract]
    public interface IServiceEbar
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
    }
}
