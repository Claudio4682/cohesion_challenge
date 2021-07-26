using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRequest.Domain.Common
{
    public static class Enums
    {
        public enum CurrentStatus
        {
            NotApplicable,
            Created,
            InProgress,
            Complete,
            Canceled
        }
    }
}
