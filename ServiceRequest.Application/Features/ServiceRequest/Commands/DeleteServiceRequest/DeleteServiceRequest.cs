using AutoMapper;
using MediatR;
using ServiceRequest.Application.Contracts.Persistence;
using ServiceRequest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRequest.Application.Features.ServiceRequest.Commands.DeleteServiceRequest
{

    public partial class DeleteServiceRequestCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteServiceRequest : IRequestHandler<DeleteServiceRequestCommand>
    {
        private readonly IAsyncRepository<Request> _serviceRequestRepository;
        private readonly IMapper _mapper;
        public DeleteServiceRequest(IMapper mapper, IAsyncRepository<Request> serviceRequestRepository)
        {
            _mapper = mapper;
            _serviceRequestRepository = serviceRequestRepository;
        }
        public async Task<Unit> Handle(DeleteServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var requestToDelete = await _serviceRequestRepository.GetByIdAsync(request.Id);
            if(requestToDelete is null)
            {
                throw new Exceptions.NotFoundException(nameof(Request), request.Id);
            }
            await _serviceRequestRepository.DeleteAsync(requestToDelete);
            return Unit.Value;
        }
    }
}
