using PCSP_Assignment_1.Core;
using PCSP_Assignment_1.Benchmarking;

//This is not representative data, we do not have JIT warmup. Just an example of output and overall working test 
var matrix = new Matrix(100);
var genTime = ActionTimer.Measure(matrix.GenerateRandomMatrix);
Console.WriteLine($"Generation time: {genTime}");
var sortTime = ActionTimer.Measure(matrix.PlaceRowMaximumOnDiagonal);
Console.WriteLine($"Sorting time: {sortTime}");
