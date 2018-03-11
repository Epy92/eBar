using System.ServiceModel;
using System.ServiceModel.Web;

namespace eBarService.ServiceInterfaces
{
    [ServiceContract]
    public interface IRestaurantService
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetResturantsByLocation/{latitude}/{longitude}/{range}/{location}")]
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
        UriTemplate = "GetAllRestaurants")]
        string GetAllRestaurants();

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

        [OperationContract]
        [WebInvoke(Method = "POST",
        BodyStyle = WebMessageBodyStyle.Bare,
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "SaveRestaurant")]
        string SaveRestaurant(Restaurants restaurant);
    }
}