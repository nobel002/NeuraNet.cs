using System;
using NeuraNet;
namespace ai
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      int[] shape = { 4, 7, 7, 7, 2 };
      const int specimenCount = 10;
      const int trainTime = (int)10e3;
      NetCreator test = new NetCreator(shape, specimenCount, trainTime);
      test.Init();
      float[][] nodes = test.nodes;
      Console.WriteLine(nodes);
    }
  }
}
