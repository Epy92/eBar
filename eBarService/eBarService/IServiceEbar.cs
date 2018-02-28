using System.ServiceModel;
using eBarService.Models;

namespace eBarService
{
    [ServiceContract]
    public interface IServiceEbar
    {

        [OperationContract]
        string Register(User user);

        [OperationContract]
        bool UserLogin(string username, string password);
    }
}
