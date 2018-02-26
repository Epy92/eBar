using System.Runtime.Serialization;
using System.ServiceModel;
using eBarService.Models;

namespace eBarService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceEbar" in both code and config file together.
    [ServiceContract]
    public interface IServiceEbar
    {

        [OperationContract]
        string Register(User user);

        [OperationContract]
        bool UserLogin(string username, string password);
    }
}
