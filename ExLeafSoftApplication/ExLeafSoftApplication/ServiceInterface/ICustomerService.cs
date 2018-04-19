using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExLeafSoftApplication.ServiceInterface
{
    public interface ICustomerService
    {
        Task<CapsuleItem> RefreshDataAsync();
        Task<bool> LoginAsync(string a, string b);
        Task<string> RegisterDataAsync();
    }

   

}
