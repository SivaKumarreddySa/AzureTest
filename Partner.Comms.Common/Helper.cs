using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Partner.Comms.Common
{
    public class Helper
    {
        public static bool IsLogErrorLevel(int statusCode)
        {
            return statusCode >= (int)HttpStatusCode.InternalServerError && statusCode <= (int)HttpStatusCode.NetworkAuthenticationRequired;
        }
    }
}