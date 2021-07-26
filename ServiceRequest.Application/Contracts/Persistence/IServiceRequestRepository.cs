using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ServiceRequest.Domain.Entities;

namespace ServiceRequest.Application.Contracts.Persistence
{
    public interface IServiceRequestRepository : IAsyncRepository<Request>
    {
  
    }
}
 