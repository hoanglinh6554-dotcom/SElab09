using BasicMath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataDrivenTests;

[TestClass]
public class DataDrivenMathTests
{
    private static IEnumerable<object[]> ReadCsv(string file)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "TestData", file);
        foreach (var line in File.ReadLines(path).Skip(1))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var p = line.Split(',');
            yield return new object[] { double.Parse(p[0]), double.Parse(p[1]), double.Parse(p[2]) };
        }
    }
    public static IEnumerable<object[]> AddRows => ReadCsv("add_testdata.csv");
    public static IEnumerable<object[]> SubtractRows => ReadCsv("subtract_testdata.csv");
    public static IEnumerable<object[]> MultiplyRows => ReadCsv("multiply_testdata.csv");
    public static IEnumerable<object[]> DivideRows => ReadCsv("divide_testdata.csv");

    private readonly BasicMaths math = new();
    [DataTestMethod, DynamicData(nameof(AddRows))] public void Add_Csv(double a,double b,double e) => Assert.AreEqual(e, math.Add(a,b));
    [DataTestMethod, DynamicData(nameof(SubtractRows))] public void Subtract_Csv(double a,double b,double e) => Assert.AreEqual(e, math.Subtract(a,b));
    [DataTestMethod, DynamicData(nameof(MultiplyRows))] public void Multiply_Csv(double a,double b,double e) => Assert.AreEqual(e, math.Multiply(a,b));
    [DataTestMethod, DynamicData(nameof(DivideRows))] public void Divide_Csv(double a,double b,double e) => Assert.AreEqual(e, math.Divide(a,b));
}
