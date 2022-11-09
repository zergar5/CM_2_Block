using System.Globalization;

namespace CM_2_Block.Tools.SolutionCheck;

public class SolutionChecker
{
    private readonly CultureInfo _culture;

    public SolutionChecker()
    {
        _culture = CultureInfo.CreateSpecificCulture("en-US");
    }

    public void CalcError(double[] x, int n)
    {
        var xStar = Generate(n);
        var error = Math.Abs(NormCalculator.CalcNorm(xStar) - NormCalculator.CalcNorm(x)) / NormCalculator.CalcNorm(xStar);
        Console.Write("Error: ");
        Console.WriteLine(error.ToString("0.00000000000000e+00", _culture));
    }
    private double[] Generate(int n)
    {
        var x = new double[n];

        switch (n)
        {
            case 945:
                var j1 = 0;

                for (var i = 0; i < n; i++)
                {
                    if (i != 0 && i % 45 == 0)
                    {
                        j1++;
                    }
                    x[i] = j1;
                }
                break;
            case 4545:
                var j02 = 0.0;
                for (var i = 0; i < n; i++)
                {
                    if (i != 0 && i % 45 == 0)
                    {
                        j02 += 0.2;
                    }
                    x[i] = j02;
                }
                break;
            default:
                for (var i = 0; i < n; i++)
                {
                    x[i] = i + 1;
                }
                break;
        }

        return x;
    }
}