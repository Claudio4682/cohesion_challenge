using AutoMapper;
using MediatR;
using ServiceRequest.Application.Contracts.Persistence;
using ServiceRequest.Application.Exceptions;
using ServiceRequest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRequest.Application.Features.ServiceRequest
{
    public class GetServiceRequestDetailQuery : IRequest<ServiceRequestVm>
    {
        public Guid Id { get; set; }
    }
    public class GetServiceRequestDetail : IRequestHandler<GetServiceRequestDetailQuery, ServiceRequestVm>
    {
        private readonly IAsyncRepository<Request> _serviceRequestRepository;
        private readonly IMapper _mapper;
        public GetServiceRequestDetail(IMapper mapper, IAsyncRepository<Request> serviceRequestRepository)
        {
            _mapper = mapper;
            _serviceRequestRepository = serviceRequestRepository;
        }
        public async Task<ServiceRequestVm> Handle(GetServiceRequestDetailQuery request, CancellationToken cancellationToken)
        {
            var serviceRequest = await _serviceRequestRepository.GetByIdAsync(request.Id);

            if (serviceRequest is null)
            {
                throw new NotFoundException(nameof(Request), request.Id);
            }
            return _mapper.Map<ServiceRequestVm>(serviceRequest);
        }
    }
}
