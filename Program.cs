﻿using System;
using NeuraNet;
namespace Program
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
      Net testNetwork = test.enviroment[0];
      double[] testInput = { 420d, 6.9d, 0.42096d, 69420d };
      testNetwork.SaveSynapses(@"D:\C# Files\ai\gen\synapses.ai");
      Console.WriteLine(testNetwork.Use(testInput)[0]);
    }
  }
}

