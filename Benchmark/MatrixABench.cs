using BenchmarkDotNet.Attributes;
using CM_2_Block.IO;
using CM_2_Block.Methods;
using CM_2_Block.Models;

namespace Benchmark;

public class MatrixABench
{
    private string _root = @"F:\Visual Studio\Projects\CM_2_Block\CM_2_Block\Input\3Point\";
    private BlockDiagMatrix _blockDiagMatrix1;
    private BlockDiagMatrix _blockDiagMatrix2;
    private BlockDiagMatrix _blockDiagMatrix5;
    private double[] _F;
    private double[] _x1;
    private double[] _x2;
    private double[] _x5;
    private double _relaxation1;
    private double _relaxation2;
    private double _relaxation5;
    private double _eps;
    private int _maxIter;
    private int _blockSize1;
    private int _blockSize2;
    private int _blockSize5;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var matrixI = new MatrixIO(_root);
        var vectorI = new VectorIO(_root);
        var parametersI = new ParametersIO(_root);

        _blockDiagMatrix1 = new BlockDiagMatrix();
        _blockDiagMatrix1.MemoryAllocated(matrixI, "matrixBlockA.txt");

        _blockDiagMatrix2 = new BlockDiagMatrix();
        _blockDiagMatrix2.MemoryAllocated(matrixI, "matrixBlockA.txt");

        _blockDiagMatrix5 = new BlockDiagMatrix();
        _blockDiagMatrix5.MemoryAllocated(matrixI, "matrixBlockA.txt");

        _F = vectorI.ReadDouble("vectorFA.txt");
        _x1 = vectorI.ReadDouble("startVector.txt");
        _x2 = vectorI.ReadDouble("startVector.txt");
        _x5 = vectorI.ReadDouble("startVector.txt");

        var tuple = parametersI.ReadMethodParameters("parameters.txt");
        _relaxation1 = 1.77;
        _relaxation2 = 1.73;
        _relaxation5 = 1.69;
        _eps = tuple.Item2;
        _maxIter = tuple.Item3;
        _blockSize1 = 1;
        _blockSize2 = 2;
        _blockSize5 = 5;
        _blockDiagMatrix1.BlockSize = _blockSize1;
        _blockDiagMatrix2.BlockSize = _blockSize2;
        _blockDiagMatrix5.BlockSize = _blockSize5;
    }

    [Benchmark]
    public void MatrixABlockSize1()
    {
        BlockRelaxation.Solve(_blockDiagMatrix1, _F, _x1, _relaxation1, _eps, _maxIter);
    }

    [Benchmark]
    public void MatrixABlockSize2()
    {
        BlockRelaxation.Solve(_blockDiagMatrix2, _F, _x2, _relaxation2, _eps, _maxIter);
    }
    [Benchmark]
    public void MatrixABlockSize5()
    {
        BlockRelaxation.Solve(_blockDiagMatrix5, _F, _x5, _relaxation5, _eps, _maxIter);
    }
}