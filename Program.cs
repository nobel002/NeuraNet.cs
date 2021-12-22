using System;
using NeuraNet;
namespace Program
{
  class Program
  {
    /// <summary>
    /// This is currently being used as a testing facility.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      int[] shape = { 4, 7, 7, 7, 2 };
      const int specimenCount = 10;
      const int trainTime = (int)10e3;
      NetCreator test = new NetCreator(shape, specimenCount, trainTime);
      Net testNetwork = test.enviroment[0];
      double[] testInput = { 420d, 6.9d, 0.42096d, 69420d };
      testNetwork.SaveSynapses(@"D:\C# Files\ai\gen\synapses.ai");
      Console.WriteLine("The first net.");
      Console.WriteLine(testNetwork.Use(testInput)[0]);
      // Test the reading of an .ai file
      Net testNetworkTwo = test.enviroment[5];
      Console.WriteLine("The original output of this net:");
      Console.WriteLine(testNetworkTwo.Use(testInput)[0]);
      Console.WriteLine("This should give the same output as the first net work... (same input)");
      testNetworkTwo.LoadSynapses(@"D:\C# Files\ai\gen\synapses.ai");
      Console.WriteLine(testNetworkTwo.Use(testInput)[0]);
    }
  }
}

