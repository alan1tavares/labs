using ConsoleApp;

namespace MSTest;

[TestClass]
public class CalculatorTests
{

    [TestMethod]
    public void CalculatorNullTest()
    {
        var calculator = new Calculator();
        Assert.IsNotNull(calculator);
    }

    [TestMethod]
    public void AddTest()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var actual = calculator.Add(1, 1);

        // Assert
        Assert.AreEqual(2, actual);
    }

    [TestMethod]
    public void SubtractTest()
    {
        var calculator = new Calculator();
        var actual = calculator.Subtract(1, 1);
        Assert.AreEqual(0, actual);
    }

    [TestMethod]
    public void MultiplyTest()
    {
        var calculator = new Calculator();
        var actual = calculator.Multiply(1, 1);
        Assert.AreEqual(1, actual);
    }


    [TestMethod]
    public void DivideByZeroTest()
    {
        var calculator = new Calculator();
        var actual = calculator.Divide(1, 0);
        Assert.IsNull(actual);
    }

    [DataTestMethod]
    [DataRow(1, 1, 2)]
    [DataRow(2, 2, 4)]
    [DataRow(3, 3, 6)]
    //[DataRow(0, 0, 1)] // The test run with this row fails
    public void AddDataDrivenTest(int x, int y, int expected)
    {
        var calculator = new Calculator();
        var actual = calculator.Add(x, y);
        Assert.AreEqual(expected, actual);
    }
}