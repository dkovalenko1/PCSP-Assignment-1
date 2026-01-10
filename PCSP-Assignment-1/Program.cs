using PCSP_Assignment_1.Core;
using PCSP_Assignment_1.Benchmarking;

//This is not representative data, we do not have JIT warmup. Just an example of output and overall working test
var matrix = new Matrix(5);
var genTime = ActionTimer.Measure(matrix.GenerateRandom);
MatrixPrinter.Print(matrix);
Console.WriteLine($"\nGeneration time: {genTime}");
var sortTime = ActionTimer.Measure(matrix.PlaceMaxOnDiagonal);
Console.WriteLine($"Sorting time: {sortTime}\n");
MatrixPrinter.Print(matrix);
