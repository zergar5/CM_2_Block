using CM_2_Block.IO;

namespace CM_2_Block.Models;

public class BlockDiagMatrix
{
    public int N { get; set; }
    public int M { get; set; }
    public int K { get; set; }

    public int BlockSize { get; set; }
    public double[,] DiagMatrix { get; set; }
    public int[] Indexes { get; set; }

    public void MemoryAllocated(MatrixIO matrixIo, string fileName)
    {
        matrixIo.ReadMatrix(this, fileName);
    }

    public void LUDecomposition()
    {
        var n = N / BlockSize;
        for (var i = 0; i < n; i++)
        {
            var k0 = i * BlockSize;
            var k1 = (i + 1) * BlockSize;
            for (var j = k0 + 1; j < k1; j++)
            {
                DiagMatrix[4, j - 1] /= DiagMatrix[3, j - 1];
                DiagMatrix[3, j] -= DiagMatrix[4, j - 1] * DiagMatrix[2, j];
            }
        }
    }
}