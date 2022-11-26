namespace Zenithar.BFF.Exceptions;

internal sealed class ClientResponseResultException : Exception
{
    public ClientResponseResultException(string? message) : base(message) { }
}