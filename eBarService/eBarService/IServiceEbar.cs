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

        #region RestaurantOperations
        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate= "GetResturantsByLocation/{latitude}/{longitude}/{range}/{location}")]
        string GetResturantsByLocation(string latitude, string longitude, string range, string location);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetRestaurantById/?restaurantId={restaurantId}")]
        string GetRestaurantById(int restaurantId);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetRestaurantsByCity/{city}")]
        string GetRestaurantsByCity(string city);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetRestaurantsByType/?typeId={typeId}")]
        string GetRestaurantsByType(int typeId);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetRestaurantTypes")]
        string GetRestaurantTypes();

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetRestaurantLocation/?restaurantId={restaurantId}")]
        string GetRestaurantLocation(int restaurantId);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetRestaurantTables/?restaurantId={restaurantId}")]
        string GetRestaurantTables(int restaurantId);
        #endregion 

    }
}
