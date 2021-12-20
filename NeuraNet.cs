using System;

namespace NeuraNet
{
  /// <summary>
  /// This class is the main class you'll be working with.
  /// Define it's shape, numberOfSpecimen, trainingTime 
  /// and then override the train function
  /// to get started with this. Don't forget to init the class to get started with your parameters.
  /// </summary>
  class NetCreator
  {
    public int[] shape { get; set; }
    public float[][] nodes { get; }
    public float[][][] synapses { get; }
    public Net[] enviroment { get; set; }
    public int numberOfSpecimen { get; set; }
    public int trainingTime { get; set; }

    public NetCreator(int[] shape, int numberOfSpecimen, int trainingTime)
    {
      this.shape = shape;
      this.numberOfSpecimen = numberOfSpecimen;
      this.trainingTime = trainingTime;
      this.Init();
    }

    /// <summary>
    /// This inits the class, you shouldn't call this but it's public for degug purposes.
    /// </summary>
    public void Init()
    {
      Console.WriteLine("Initing");
      CreateNodes();
    }

    /// <summary>
    /// This creates the nodes in this object.
    /// </summary>
    void CreateNodes()
    {
      //ToDO(nobel) check wheter it should be -1 or not.
      for (int i = 0; i <= shape.Length; i++)
      {
        this.nodes[i] = new float[this.shape[i]];
        Array.Clear(this.nodes[i], 0, this.shape[i]);
      }
    }
  }

  class Net
  {
    public int score { get; set; }
    public float[][][] synapses { get; set; }
  }
}