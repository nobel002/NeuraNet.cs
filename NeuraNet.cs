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
    public float[][] nodes { get; set; }
    public float[][][] synapses { get; set; }
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
      Console.WriteLine("Initing...");
      // Console.WriteLine(this.shape);
      CreateNodes();
      Console.WriteLine("Created Nodes...");
      CreateSynapses();
      Console.WriteLine("Created synapses...");
    }

    /// <summary>
    /// This creates the nodes in this object.
    /// </summary>
    void CreateNodes()
    {
      this.nodes = new float[this.shape.Length][];
      for (int i = 0; i <= shape.Length - 1; i++)
      {
        this.nodes[i] = new float[this.shape[i]];
        //Array.Clear(this.nodes[i], 0, this.shape[i]);
      }
    }

    void CreateSynapses()
    {
      this.synapses = new float[this.shape.Length - 1][][];
      for (int i = 0; i <= this.shape.Length - 2; i++)
      {
        this.synapses[i] = new float[this.shape[i + 1]][];
        for (int j = 0; j <= this.synapses[i].Length - 1; j++)
        {
          this.synapses[i][j] = new float[this.shape[i]];
          //Array.Clear(this.synapses[i][j], 0, this.shape[i]);
        }
      }
    }
  }

  class Net
  {
    public int score { get; set; }
    public float[][][] synapses { get; set; }
  }
}