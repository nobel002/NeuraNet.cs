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
    public double[][] nodes { get; set; }
    public double[][][] synapses { get; set; }
    public Net[] enviroment { get; set; }
    public int numberOfSpecimen { get; set; }
    public int trainingTime { get; set; }
    private Random rnd = new Random();

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
      CreateNodes();
      //FillNodes();
      Console.WriteLine("Created Nodes...");
      CreateSynapses();
      FillSynapses();
      Console.WriteLine("Created Synapses...");
    }

    /// <summary>
    /// This creates the nodes in this object.
    /// And fills them with all 0's.
    /// </summary>
    void CreateNodes()
    {
      this.nodes = new double[this.shape.Length][];
      for (int i = 0; i <= shape.Length - 1; i++)
      {
        this.nodes[i] = new double[this.shape[i]];
        //Array.Clear(this.nodes[i], 0, this.shape[i]);
      }
    }

    /// <summary>
    /// Creates the synapses and fills them with 0's;
    /// </summary>
    void CreateSynapses()
    {
      this.synapses = new double[this.shape.Length - 1][][];
      for (int i = 0; i <= this.shape.Length - 2; i++)
      {
        this.synapses[i] = new double[this.shape[i + 1]][];
        for (int j = 0; j <= this.synapses[i].Length - 1; j++)
        {
          this.synapses[i][j] = new double[this.shape[i]];
          //Array.Clear(this.synapses[i][j], 0, this.shape[i]);
        }
      }
    }

    /// <summary>
    /// Fills the synapses with noise.
    /// </summary>
    void FillSynapses()
    {
      for (int i = 0; i <= this.synapses.Length - 1; i++)
      {
        for (int j = 0; j <= this.synapses[i].Length - 1; j++)
        {
          for (int k = 0; k <= this.synapses[i][j].Length - 1; k++)
          {
            this.synapses[i][j][k] = rnd.NextDouble();
          }
        }
      }
    }

    public Net[] CreateEnv()
    {
      for (int i = 0; i <= numberOfSpecimen - 1; i++)
      {
        this.enviroment[i] = new Net(this.nodes, this.synapses);
        // To ensure that we generate different Nets each time.
        this.FillSynapses();
      }
      return this.enviroment;
    }
  }

  class Net
  {
    public int score { get; set; }
    public double[][] nodes { get; set; }
    public double[][][] synapses { get; set; }

    public Net(double[][] nodes, double[][][] synapses)
    {
      this.nodes = nodes;
      this.synapses = synapses;
    }

    void Use(){
      //TODO(nobel) write the use function so we can calculate everything.
    }
  }
}