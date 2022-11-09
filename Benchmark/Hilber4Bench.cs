using BenchmarkDotNet.Attributes;
using CM_2_Block.IO;
using CM_2_Block.Methods;
using CM_2_Block.Models;

namespace Benchmark;

public class Hilber4Bench
{
    private string _root = @"F:\Visual Studio\Projects\CM_2_Block\CM_2_Block\Input\4Point\";
    private BlockDiagMatrix _blockDiagMatrix1;
    private double[] _F;
    private double[] _x1;
    private double _relaxation1;
    private double _eps;
    private int _maxIter;
    private int _blockSize1;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var matrixI = new MatrixIO(_root);
        var vectorI = new VectorIO(_root);
        var parametersI = new ParametersIO(_root);

        _blockDiagMatrix1 = new BlockDiagMatrix();
        _blockDiagMatrix1.MemoryAllocated(matrixI, "hilbert4.txt");

        _F = vectorI.ReadDouble("hilbertF4.txt");
        _x1 = vectorI.ReadDouble("startVector.txt");

        var tuple = parametersI.ReadMethodParameters("parameters.txt");
        _relaxation1 = 0.01;
        _eps = tuple.Item2;
        _maxIter = tuple.Item3;
        _blockSize1 = 1;
        _blockDiagMatrix1.BlockSize = _blockSize1;
    }

    [Benchmark]
    public void Hilbert4BlockSize1()
    {
        BlockRelaxation.Solve(_blockDiagMatrix1, _F, _x1, _relaxation1, _eps, _maxIter);
    }
}