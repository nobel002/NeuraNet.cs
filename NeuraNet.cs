using System;
using System.IO;

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
      this.nodes = new double[this.shape.Length][];
      this.synapses = new double[this.shape.Length - 1][][];
      this.enviroment = new Net[numberOfSpecimen];
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
      CreateEnv();
      Console.WriteLine("Created the enviroment with all the Nets.");
    }

    /// <summary>
    /// This creates the nodes in this object.
    /// And fills them with all 0's.
    /// </summary>
    void CreateNodes()
    {

      for (int i = 0; i <= shape.Length - 1; i++)
      {
        this.nodes[i] = new double[this.shape[i]];
        Array.Clear(this.nodes[i], 0, this.shape[i]);
      }
    }

    /// <summary>
    /// Creates the synapses and fills them with 0's;
    /// </summary>
    void CreateSynapses()
    {
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
    //TO_DO(nobel) write the use function so we can calculate everything.

    /// <summary>
    /// This uses a specific net. Meaning it calculates the weighted sum over the whole net.
    /// </summary>
    /// <param name="inputs">
    /// This is a double[] with the same length as shape[0].
    /// </param>
    /// <returns>
    /// The output array. This will return an array where the numbers of the last nodes are stored. So you can process them however you like.
    /// </returns>
    /// <exception cref="Exception">
    /// This trows an exception if the input array of the net and the provided input array don't match in length.
    /// </exception>
    public double[] Use(double[] inputs)
    {
      if (inputs.Length == this.nodes[0].Length)
      {
        for (int i = 0; i <= this.nodes[0].Length - 1; i++)
        {
          this.nodes[0][i] = inputs[i];
        }

        // First We loop over all the nodes.
        for (int i = 1; i <= this.nodes.Length - 1; i++)
        {
          for (int j = 0; j <= this.nodes[i].Length - 1; j++)
          {
            //Now loop over all the synapses.
            for (int synapsIndex = 0; synapsIndex <= this.synapses[i - 1][j].Length - 1; synapsIndex++)
            {
              this.nodes[i][j] += this.nodes[i - 1][synapsIndex] * this.synapses[i - 1][j][synapsIndex];
            }
          }
        }


        // Returns the last array as the output so you can use it as you like.
        return this.nodes[this.nodes.Length - 1];
      }
      else
      {
        throw new Exception("The input array you provided is not the right length.");
      }
    }

    /// <summary>
    /// This sets the synapses of a specific Net instance. Make shure that the shape matches the shape it already has.
    /// </summary>
    /// <param name="synapses">
    /// double[][][] the synapses array you want the net to have.
    /// </param>
    public void SetSynapses(double[][][] synapses)
    {
      //ToDo(Nobel) check whether the synapses and this.synapses match in shape.
      this.synapses = synapses;
    }

    /// <summary>
    /// This writes the synapses to a file.
    /// </summary>
    /// <param name="path">
    /// The path to the file where we can save the synapses.
    /// </param>
    /// <returns>
    /// For good mesures it also returns the synaps array inorder to process it further.
    /// </returns>
    public double[][][] SaveSynapses(string path)
    {
      //TO.DO(nobel)fic this so it prints as a python array.
      using (FileStream fs = File.Open(path, System.IO.FileMode.OpenOrCreate))
      {
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i <= this.synapses.Length - 1; i++)
        {
          for (int j = 0; j <= this.synapses[i].Length - 1; j++)
          {
            for (int k = 0; k <= this.synapses[i][j].Length - 1; k++)
            {
              sw.Write(this.synapses[i][j][k]);
              if (k < this.synapses[i][j].Length - 1)
                sw.Write(';');
            }
            if (j < this.synapses[i].Length - 1)
              sw.Write('|');
          }
          sw.Write('\n');
        }
      }
      return this.synapses;
    }
  }

}