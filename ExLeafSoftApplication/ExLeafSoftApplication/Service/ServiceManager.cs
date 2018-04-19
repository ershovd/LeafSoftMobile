using ExLeafSoftApplication.ServiceInterface;
using System.Threading.Tasks;

namespace ExLeafSoftApplication.Data
{
    public class ServiceManager
    {
        ICustomerService restService;

        public ServiceManager(ICustomerService service)
        {
            restService = service;
        }

        public Task<CapsuleItem> GetTasksAsync()
        {
            return restService.RefreshDataAsync();
        }

        public Task<bool> GetTaskLoginResult(string username, string password)
        {
            return restService.LoginAsync(username, password);
        }


        public Task<string> PostRegister()
        {
            return restService.RegisterDataAsync();
        }
    }

  


}
