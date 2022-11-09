using CM_2_Block.IO;
using CM_2_Block.Methods;
using CM_2_Block.Models;
using CM_2_Block.Tools.SolutionCheck;

var matrixI = new MatrixIO("../CM_2_Block/Input/3Point/");
var vectorI = new VectorIO("../CM_2_Block/Input/3Point/");
var vectorO = new VectorIO("../CM_2_Block/Output/");
var parametersI = new ParametersIO("../CM_2_Block/Input/3Point/");

var blockDiagMatrix = new BlockDiagMatrix();
blockDiagMatrix.MemoryAllocated(matrixI, "matrixBlockB.txt");

var F = vectorI.ReadDouble("vectorFB.txt");
var startX = vectorI.ReadDouble("startVector.txt");
var x = new double[startX.Length];

var (relaxation, eps, maxIter, blockSize) = parametersI.ReadMethodParameters("parameters.txt");

blockDiagMatrix.BlockSize = blockSize;

var solutionChecker = new SolutionChecker();

Array.Copy(startX, x, startX.Length);

BlockRelaxation.Solve(blockDiagMatrix, F, x, relaxation, eps, maxIter);

solutionChecker.CalcError(x, blockDiagMatrix.N);
vectorO.Write(x, "blockRelaxationOutput.txt");