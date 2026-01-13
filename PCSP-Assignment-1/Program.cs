using PCSP_Assignment_1.Core;
using PCSP_Assignment_1.Benchmarking;

//This is not representative data, we do not have JIT warmup. Just an example of output and overall working test
var matrix = new Matrix(10000);
var genTime = ActionTimer.Measure(matrix.GenerateRandomSeq);
Console.WriteLine($"\nGeneration time seq: {genTime}");
var sortTime = ActionTimer.Measure(matrix.PlaceMaxOnDiagonalSeq);
Console.WriteLine($"Sorting time seq: {sortTime}");
Console.WriteLine($"Total time seq: {genTime +  sortTime}\n");

var matrix1 = new Matrix(10000);
var genTime1 = ActionTimer.Measure(() => matrix1.GenerateRandomParallel(8));
Console.WriteLine($"Generation time parallel: {genTime1}");
var sortTime1 = ActionTimer.Measure(() => matrix1.PlaceMaxOnDiagonalParallel(8));
Console.WriteLine($"Sorting time in parallel: {sortTime1}");
Console.WriteLine($"Total time in parallel: {genTime1 +  sortTime1}");
