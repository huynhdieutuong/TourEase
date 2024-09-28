namespace BuildingBlocks.Shared.Exceptions;
public class ForBidException : ApplicationException
{
    public ForBidException() : base("Invalid owner.")
    {
    }

    public ForBidException(string message) : base(message)
    {
    }
}
