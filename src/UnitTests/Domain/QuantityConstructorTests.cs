using Domain.Exceptions;
using Domain.Primitives;

namespace UnitTests.Domain;

public class QuantityConstructorTests
{
    [Fact]
    public void Zero_ThrowException()
    {
        Assert.Throws<DomainValidationException>(() => new Quantity(0));
    }

    [Fact]
    public void Negative_ThrowsException()
    {
        Assert.Throws<DomainValidationException>(() => new Quantity(-1));
    }

    [Fact]
    public void Positive_CreateInstance()
    {
        var result = new Quantity(1);
    }
}