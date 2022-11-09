using CM_2_Block.Methods;
using CM_2_Block.Models;

namespace CM_2_Block.Tools;

public class Iterator
{
    public static double NextIteration(BlockDiagMatrix blockDiagMatrix, double[] currentX, double[] F,
        double relaxation)
    {
        var n = blockDiagMatrix.N;
        var blockSize = blockDiagMatrix.BlockSize;
        var matrix = blockDiagMatrix.DiagMatrix;
        var indexes = blockDiagMatrix.Indexes;

        var residual = 0.0;
        var sumOfSqVecF = 0.0;
        var nBlocks = n / blockSize;
        for (var i = 0; i < nBlocks; i++)
        {
            var k0 = i * blockSize;
            var k1 = (i + 1) * blockSize;
            var r = BlockRelaxation.CalcBlockPart(blockDiagMatrix, currentX, k0, k1);
            var bi = 0;
            for (var j = k0; j < k1; j++, bi++)
            {
                var sum = 0.0;
                for (var k = 0; k < 7; k++)
                {
                    if (indexes[k] + j < 0 || indexes[k] + j >= n) continue;
                    if (indexes[k] + j < k0 || indexes[k] + j >= k1)
                    {
                        sum += matrix[k, j] * currentX[indexes[k] + j];
                    }
                }
                r[bi] = F[j] - (r[bi] + sum);
                residual += (r[bi]) * (r[bi]);
                r[bi] *= relaxation;
                sumOfSqVecF += F[j] * F[j];
            }
            SLAESolver.SolveSLAE(blockDiagMatrix, r, relaxation, k0, k1);
            bi = 0;
            for (var j = k0; j < k1; j++, bi++)
            {
                currentX[j] += r[bi];
            }
        }
        return Math.Sqrt(residual) / Math.Sqrt(sumOfSqVecF);
    }
}
