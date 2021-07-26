using AutoMapper;
using MediatR;
using ServiceRequest.Application.Contracts.Infrastructure;
using ServiceRequest.Application.Contracts.Persistence;
using ServiceRequest.Application.Models;
using ServiceRequest.Domain.Common;
using ServiceRequest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceRequest.Application.Features.ServiceRequest.Commands.UpdateServiceRequest
{
    public partial class UpdateServiceRequestCommand : IRequest<ServiceRequestVm>
    {
        public Guid Id { get; set; }
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string LastModifiedBy { get; set; }
    }
    public class UpdateServiceRequest : IRequestHandler<UpdateServiceRequestCommand, ServiceRequestVm>
    {
        private readonly IAsyncRepository<Request> _serviceRequestRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public UpdateServiceRequest(IMapper mapper, IAsyncRepository<Request> serviceRequestRepository, IEmailService emailService)
        {
            _mapper = mapper;
            _serviceRequestRepository = serviceRequestRepository;
            _emailService = emailService;
        }
        public async Task<ServiceRequestVm> Handle(UpdateServiceRequestCommand request, CancellationToken cancellationToken)
        {
            var requestToUpdate = await _serviceRequestRepository.GetByIdAsync(request.Id);
            if(requestToUpdate is null)
            {
                throw new Exceptions.NotFoundException(nameof(Request), request.Id);
            }

            var validator = new UpdateServiceRequestValidator();
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validatorResult);
            }

            _mapper.Map(request, requestToUpdate, typeof(UpdateServiceRequestCommand), typeof(Request));
            var requestUpdated = await _serviceRequestRepository.UpdateAsync(requestToUpdate);
            if(requestUpdated.CurrentStatus == Enums.CurrentStatus.Complete || requestUpdated.CurrentStatus == Enums.CurrentStatus.Canceled)
            {
                var email = new Email() { To = "", Body = "your request was closed", Subject = $"request {request.Id} - {request.Description} was closed" };
                await _emailService.SendEmail(email);
            }

            return _mapper.Map<ServiceRequestVm>(requestUpdated); ;
        }
    }
}
