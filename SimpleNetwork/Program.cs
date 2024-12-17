
using TicTacToe;

Console.WriteLine("Program Started.");


#region Generate Net

var network = new Net(2, 1, 2);

//network.Train(trainingData, 0.05);

#endregion

#region Test and Display Results
Console.WriteLine("Commencing testing...");


Console.WriteLine("Finished.");
Console.ReadLine();
#endregion