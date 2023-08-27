using System;

namespace Domain.Exceptions;
public class InvalidBlockException : Exception
{
    public InvalidBlockException(string message) : base(message) { }
}
