using PCSP_Assignment_1.Core;
using PCSP_Assignment_1.Benchmarking;

//This is not representative data, we do not have JIT warmup. Just an example of output and overall working test
var matrix = new Matrix(10000);
var genTime = ActionTimer.Measure(matrix.GenerateRandomSeq);
Console.WriteLine($"\nGeneration time seq: {genTime}");
var sortTime = ActionTimer.Measure(matrix.PlaceMaxOnDiagonalSeq);
Console.WriteLine($"Sorting time seq: {sortTime}\n");

var matrix1 = new Matrix(5);
var genTime1 = ActionTimer.Measure(() => matrix1.GenerateRandomParallel(1));
Console.WriteLine($"\nGeneration time parallel: {genTime1}");
MatrixPrinter.Print(matrix1);


