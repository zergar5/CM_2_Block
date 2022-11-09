using CM_2_Block.Models;

namespace CM_2_Block.IO;

public class MatrixIO
{
    private readonly string _path;

    public MatrixIO(string path)
    {
        _path = path;
    }

    public void ReadMatrix(BlockDiagMatrix blockDiagMatrix, string fileName)
    {
        using var streamReader = new StreamReader(_path + fileName);
        var sizes = streamReader.ReadLine().Split(' ');
        blockDiagMatrix.N = int.Parse(sizes[0]);
        blockDiagMatrix.M = int.Parse(sizes[1]);
        blockDiagMatrix.K = int.Parse(sizes[2]);
        blockDiagMatrix.Indexes = streamReader.ReadLine().Split().Select(int.Parse).ToArray();
        var matrix = new double[7, blockDiagMatrix.N];
        for (var i = 0; i < 7; i++)
        {
            var line = streamReader.ReadLine().Replace('.', ',').Split(' ');
            for (var j = 0; j < blockDiagMatrix.N; j++)
            {
                matrix[i, j] = double.Parse(line[j]);
            }
        }
        blockDiagMatrix.DiagMatrix = matrix;
    }
}