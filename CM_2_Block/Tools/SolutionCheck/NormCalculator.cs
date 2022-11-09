namespace CM_2_Block.Tools.SolutionCheck;

public class NormCalculator
{
    public static double CalcNorm(double[] vector)
    {
        var n = vector.Length;
        var result = 0.0;
        for (var i = 0; i < n; i++)
        {
            result += vector[i] * vector[i];
        }
        return result;
    }
}