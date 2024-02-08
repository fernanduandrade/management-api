namespace Shop.Domain.Products.Errors;

public class InvalidProductException : Exception
{
    public InvalidProductException() {}
    public InvalidProductException(string message) : base(message) {}
}