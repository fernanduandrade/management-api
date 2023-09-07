using System;

namespace Shop.Domain.Errors;
public class InvalidProductException : Exception
{
    public InvalidProductException() {}
    public InvalidProductException(string message) : base(message) {}
}
