using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class AssertException : Exception
    {
        public AssertException(string message)
            : base(message)
        {
        }
    }
}
