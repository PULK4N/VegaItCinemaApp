using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CinemaAppServices.Exceptions
{
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException(string message) : base(message)
        {

        }
    }
}