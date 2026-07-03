using System;

namespace Task.Common.Exceptions
{
    public sealed class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message)
            : base(message)
        {
        }
    }
}
