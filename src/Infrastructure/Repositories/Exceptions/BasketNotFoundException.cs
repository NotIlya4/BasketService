namespace Infrastructure.Repositories;

public class BasketNotFoundException : Exception
{
    public BasketNotFoundException() : base($"Specified basket not found")
    {
        
    }
}