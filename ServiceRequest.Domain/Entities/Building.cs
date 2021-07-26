using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRequest.Domain.Entities
{
    public class Building
    {
        public string BuildingCode { get; set; }
        public string Name { get; set; }
        public IEnumerable<Request> Requests { get; set; }
    }
}
