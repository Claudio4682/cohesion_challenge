using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRequest.Application.Features.ServiceRequest
{
    public class ServiceRequestVm
    {
        public Guid Id { get; set; }
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
