using BasicMath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicMathTests;

[TestClass]
public class BasicMathTestsClass
{
    private readonly BasicMaths _math = new();

    [DataTestMethod]
    [DataRow(1d, 1d, 2d)] [DataRow(-1d, -1d, -2d)] [DataRow(0d, 0d, 0d)]
    [DataRow(2147483647d, 1d, 2147483648d)] [DataRow(-2147483648d, -1d, -2147483649d)]
    public void Add_ReturnsExpected(double a, double b, double expected) => Assert.AreEqual(expected, _math.Add(a, b));

    [DataTestMethod]
    [DataRow(5d, 3d, 2d)] [DataRow(-5d, -3d, -2d)] [DataRow(0d, 0d, 0d)]
    [DataRow(-2147483648d, 1d, -2147483649d)]
    public void Subtract_ReturnsExpected(double a, double b, double expected) => Assert.AreEqual(expected, _math.Subtract(a, b));

    [DataTestMethod]
    [DataRow(2d, 3d, 6d)] [DataRow(-2d, -3d, 6d)] [DataRow(0d, 5d, 0d)]
    [DataRow(2147483647d, 1d, 2147483647d)]
    public void Multiply_ReturnsExpected(double a, double b, double expected) => Assert.AreEqual(expected, _math.Multiply(a, b));

    [DataTestMethod]
    [DataRow(6d, 3d, 2d)] [DataRow(-6d, -3d, 2d)] [DataRow(0d, 1d, 0d)]
    [DataRow(2147483647d, 1d, 2147483647d)]
    public void Divide_ReturnsExpected(double a, double b, double expected) => Assert.AreEqual(expected, _math.Divide(a, b));

    [TestMethod]
    public void Divide_ByZero_Throws() => Assert.ThrowsException<DivideByZeroException>(() => _math.Divide(10, 0));
}
