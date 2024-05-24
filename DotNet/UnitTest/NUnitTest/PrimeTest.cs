using ConsoleApp;

namespace NUnitTest;

public class PrimeTest
{
    private Prime _prime;

    [SetUp]
    public void Setup()
    {
        _prime = new();
    }

    [Test]
    public void IsPrime()
    {
        var result = _prime.IsPrime(1);
        Assert.That(result, Is.False, "1 should not be prime");
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(1)]
    public void IsPrimeTestCase(int value)
    {
        var result = _prime.IsPrime(value);
        Assert.That(result, Is.False, $"{value} should not be prime");
    }
}