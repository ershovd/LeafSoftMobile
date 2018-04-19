using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExLeafSoftApplication.ServiceInterface
{
    public interface IPhotoService
    {
        Task<string> SendPhotoAsync();
    
    }

   

}
