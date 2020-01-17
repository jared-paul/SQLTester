using System;

namespace Tester.src.Common.Exceptions
{
    class OperationNotSupported : Exception
    {
        public OperationNotSupported(String message) : base(message)
        {

        }
    }
}
