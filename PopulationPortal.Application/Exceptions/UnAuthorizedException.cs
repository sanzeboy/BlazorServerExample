using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PopulationPortal.Application.Exceptions
{
    public class UnAuthorizedException : BaseException
    {
        public UnAuthorizedException(string message, string description = "") : base(message, description, (int)HttpStatusCode.Unauthorized)
        {
        }
    }
}
