namespace Database.Implementations.Internal.Utility
{
    using System;

    public class DuplicateColumnNameException : Exception
    {
        public DuplicateColumnNameException(string message) : base(message)
        {
        }
    }
}