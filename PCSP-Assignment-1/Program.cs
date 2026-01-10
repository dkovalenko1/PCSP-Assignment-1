using PCSP_Assignment_1.Core;

var matrix = new Matrix(Random.Shared.Next(2, 10));
matrix.GenerateRandomMatrix();
MatrixPrinter.Print(matrix);
matrix.PlaceRowMaximumOnDiagonal();
MatrixPrinter.Print(matrix);