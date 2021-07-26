using AutoMapper;
using MediatR;
using ServiceRequest.Application.Contracts.Infrastructure;
using ServiceRequest.Application.Contracts.Persistence;
using ServiceRequest.Application.Models;
using ServiceRequest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRequest.Application.Features.ServiceRequest.Commands.CreateServiceRequest
{

    public partial class AddServiceRequestCommand : IRequest<Guid>
    {
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
    }

    public class AddServiceRequest : IRequestHandler<AddServiceRequestCommand, Guid>
    {
        private readonly IAsyncRepository<Request> _serviceRequestRepository;
        private readonly IMapper _mapper;
        public AddServiceRequest(IMapper mapper, IAsyncRepository<Request> serviceRequestRepository)
        {
            _mapper = mapper;
            _serviceRequestRepository = serviceRequestRepository;
            
        }
        public async Task<Guid> Handle(AddServiceRequestCommand request, CancellationToken cancellationToken)
        {

            var validator = new AddServiceRequestValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if(validatorResult.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validatorResult);
            }

            var serviceRequest = _mapper.Map<Request>(request);
            var serviceAdded = await _serviceRequestRepository.AddAsync(serviceRequest);

           
            return serviceAdded.Id;
        }
    }
}
