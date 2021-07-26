using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceRequest.Application.Features.ServiceRequest;
using ServiceRequest.Application.Features.ServiceRequest.Commands.CreateServiceRequest;
using ServiceRequest.Application.Features.ServiceRequest.Commands.DeleteServiceRequest;
using ServiceRequest.Application.Features.ServiceRequest.Commands.UpdateServiceRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceRequest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ServiceRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "servicerequest")]
        public async Task<ActionResult<IEnumerable<ServiceRequestVm>>> GetAllRequest()
        {
            var data = await _mediator.Send(new GetServiceRequestListQuery());
            if (data.Count == 0)
                return NoContent();

            return Ok(data);
        }

        [HttpGet("{id}", Name = "GetRequstById")]
        public async Task<ActionResult<ServiceRequestVm>> GetRequestById(Guid id)
        {
            var getRequestDetailQuery = new GetServiceRequestDetailQuery() { Id = id };
            var response = await _mediator.Send(getRequestDetailQuery);
            return Ok(response);
        }

        [HttpPost(Name = "AddRequest")]
        public async Task<ActionResult<Guid>> Create([FromBody] AddServiceRequestCommand addServiceRequestCommand)
        {
            var id = await _mediator.Send(addServiceRequestCommand);
            return CreatedAtRoute("GetRequstById", new { id = id}, addServiceRequestCommand);
        }

        [HttpPut(Name = "UpdateRequest")]
        public async Task<ActionResult> Update([FromBody] UpdateServiceRequestCommand updateServiceRequestCommand)
        {
            var requestUpdated = await _mediator.Send(updateServiceRequestCommand);
            return Ok(requestUpdated);
        }

        [HttpDelete("{id}", Name = "DeleteEvent")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteRequestCommand = new DeleteServiceRequestCommand() { Id = id };
            await _mediator.Send(deleteRequestCommand);
            return Ok();
        }
    }
}
