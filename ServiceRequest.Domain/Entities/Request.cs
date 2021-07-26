using ServiceRequest.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRequest.Domain.Entities
{
    public class Request : AuditableEntity
    {
        public Guid Id { get; set; }
        public string BuildingCode { get; set; }
        public Building Building { get; set; }
        public string Description { get; set; }
        public Enums.CurrentStatus CurrentStatus { get; set; }
    }
}
