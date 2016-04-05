namespace Database.Implementations.Internal.Utility
{
    using System;

    public class TableAlreadyExistsException : Exception
    {
        public TableAlreadyExistsException(string message) : base(message)
        {
        }
    }
}