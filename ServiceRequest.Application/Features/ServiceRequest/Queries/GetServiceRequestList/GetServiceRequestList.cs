using AutoMapper;
using MediatR;
using ServiceRequest.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceRequest.Domain.Entities;
using System.Linq;

namespace ServiceRequest.Application.Features.ServiceRequest
{
    public class GetServiceRequestListQuery : IRequest<List<ServiceRequestVm>>
    {

    }
    public class GetServiceRequestList : IRequestHandler<GetServiceRequestListQuery, IEnumerable<ServiceRequestVm>>
    {
        private readonly IAsyncRepository<Request> _serviceRequestRepository;
        private readonly IMapper _mapper;
        public GetServiceRequestList(IMapper mapper, IAsyncRepository<Request> serviceRequestRepository)
        {
            _mapper = mapper;
            _serviceRequestRepository = serviceRequestRepository;
        }
        public async Task<IEnumerable<ServiceRequestVm>> Handle(GetServiceRequestListQuery request, CancellationToken cancellationToken)
        {
            var allRequests = (await _serviceRequestRepository.ListAllAsync()).OrderBy( x => x.CreatedDate);
            return _mapper.Map<IEnumerable<ServiceRequestVm>>(allRequests);
        }
    }
}
