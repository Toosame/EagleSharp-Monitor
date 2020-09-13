using System;
using System.Collections.Generic;
using System.Text;

namespace EagleClient.Infrastructure.Exceptions
{
    public class ReadDeviceInfoException : Exception
    {
        public ReadDeviceInfoException(string message) : base(message)
        {
        }

        public ReadDeviceInfoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
