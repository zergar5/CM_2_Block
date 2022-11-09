using CM_2_Block.Models;
using CM_2_Block.Tools;
using System.Globalization;

namespace CM_2_Block.Methods;

public class BlockRelaxation
{
    public static void Solve(BlockDiagMatrix blockDiagMatrix, double[] F, double[] x, double relaxation, double eps,
        int max_iter)
    {
        Console.WriteLine("Block Relaxation");
        blockDiagMatrix.LUDecomposition();
        var residual = double.MaxValue;
        for (var i = 1; i <= max_iter && residual > eps; i++)
        {
            residual = Iterator.NextIteration(blockDiagMatrix, x, F, relaxation);
            Log(i, residual);
        }
        Console.WriteLine();
    }

    public static void Log(int i, double residual)
    {
        Console.Write("Iteration: {0} Residual: {1}   \r", i, residual.ToString("0.00000000000000e+00", CultureInfo.CreateSpecificCulture("en-US")));
    }

    public static double[] CalcBlockPart(BlockDiagMatrix blockDiagMatrix, double[] x, int k0, int k1)
    {
        var n = blockDiagMatrix.N;
        var blockSize = blockDiagMatrix.BlockSize;
        var matrix = blockDiagMatrix.DiagMatrix;
        var indexes = blockDiagMatrix.Indexes;
        var r = new double[blockSize];
        var k = 0;
        for (var i = k0; i < k1; i++, k++)
        {
            var sum = 0.0;
            for (var j = 3; j < 5; j++)
            {
                if (indexes[j] + i < 0 || indexes[j] + i >= n) continue;
                if (indexes[j] + i < k0 || indexes[j] + i >= k1) continue;
                if (j == 3)
                {
                    sum += 1.0 * x[indexes[j] + i];
                }
                else
                {
                    sum += matrix[j, i] * x[indexes[j] + i];
                }
            }
            r[k] = sum;
        }
        var buf = new double[blockSize];
        Array.Copy(r, buf, blockSize);
        k = 0;
        for (var i = k0; i < k1; i++, k++)
        {
            var sum = 0.0;
            for (var j = 2; j < 4; j++)
            {
                if (indexes[j] + i < 0 || indexes[j] + i >= n) continue;
                if (indexes[j] + i >= k0 && indexes[j] + i < k1)
                {
                    sum += matrix[j, i] * buf[indexes[j] + k];
                }
            }
            r[k] = sum;
        }

        return r;
    }
}