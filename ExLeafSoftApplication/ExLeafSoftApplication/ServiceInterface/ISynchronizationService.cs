using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExLeafSoftApplication.ServiceInterface
{
    public interface ISynchronizationService
    {
      
        Task<List<CompactCustomerModel>> SendLocalFarmerAsync();
        Task<List<ServerCustomerModel>> FetchServerFarmerAsync();
        Task<List<CompactCustomerModel>> SendUpdatedFarmerAsync();
        Task<List<CompactFieldModel>> SendLocalFieldAsync();
        Task<List<CompactFieldModel>> SendUpdatedFieldAsync();
        Task<List<ServerFieldModel>> FetchServerFieldAsync();
    }

   

}
