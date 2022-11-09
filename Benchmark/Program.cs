using Benchmark;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<MatrixABench>();
BenchmarkRunner.Run<MatrixBBench>();
BenchmarkRunner.Run<Hilber4Bench>();