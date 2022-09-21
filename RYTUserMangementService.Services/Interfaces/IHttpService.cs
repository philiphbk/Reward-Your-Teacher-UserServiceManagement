using RYTUserManagementService.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RYTUserMangementService.Services.Interfaces
{
    public interface IHttpService
    {
        Task<T> SendPostRequest<T, U>(JsonContentPostRequest<U> request);
    }
}
