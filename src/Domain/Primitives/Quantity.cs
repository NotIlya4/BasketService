using Domain.Exceptions;

namespace Domain.Primitives;

public record Quantity
{
    public int Value { get; }

    public Quantity(int value)
    {
        if (!(value >= 0))
        {
            throw new DomainValidationException("Quantity must be bigger than 0");
        }

        Value = value;
    }
}