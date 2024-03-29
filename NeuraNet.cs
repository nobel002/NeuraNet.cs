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
    public float[][] nodes { get; set; }
    public float[][][] synapses { get; set; }
    public Net[] enviroment { get; set; }
    public int numberOfSpecimen { get; set; }
    public int trainingTime { get; set; }
    //Not shure if these are needed but hey
    public float minMutationStrength = 0f;
    public float maxMutationStrength = 1f;
    public float learingRate = 0.5f; //Not yet implemented
    private Random rnd = new Random();

    public NetCreator(int[] shape, int numberOfSpecimen, int trainingTime)
    {
      this.shape = shape;
      this.numberOfSpecimen = numberOfSpecimen;
      this.trainingTime = trainingTime;
      this.nodes = new float[this.shape.Length][];
      this.synapses = new float[this.shape.Length - 1][][];
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
      // FillSynapses();
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
        this.nodes[i] = new float[this.shape[i]];
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
        this.synapses[i] = new float[this.shape[i + 1]][];
        for (int j = 0; j <= this.synapses[i].Length - 1; j++)
        {
          this.synapses[i][j] = new float[this.shape[i]];
          //Array.Clear(this.synapses[i][j], 0, this.shape[i]);
        }
      }
    }

    /*
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
              this.synapses[i][j][k] = rnd.NextFloat();
            }
          }
        }
      }
      */
    public Net[] CreateEnv()
    {
      for (int i = 0; i <= numberOfSpecimen - 1; i++)
      {
        // Console.WriteLine(Convert.ToString(i) + ',');
        // this.FillSynapses(i + 1);
        this.enviroment[i] = new Net(this.shape, this.nodes, this.synapses);
        // To ensure that we generate different Nets each time.
        // rnd.NextFloat();

      }
      Console.Write('\n');
      return this.enviroment;
    }

    /// <summary>
    /// Sorts the enviroment from best to worst preforming Net.
    /// </summary>
    // ToDo(nobel002) implement Sort
    void Sort()
    {
      this.enviroment = this.enviroment;
    }
    /// <summary>
    /// Trains the Networks, it does this for a specified amount of time.
    /// </summary>
    /// <param name="index">This sould be a number that is inside the enviroment range. An specifies which network you want to return after its done.</param>
    /// <returns>This returns the indexth Network we got</returns>
    public Net Train(int index)
    {
      for (int i = 0; i < this.trainingTime; i++)
      {
        for (int j = 0; j < this.enviroment.Length - 1; j++)
        {
          this.enviroment[j].Score();
        }
        Sort();
        float maxScore = this.enviroment[0].score;
        float minScore = this.enviroment[this.enviroment.Length - 1].score;
        for (int j = 0; j < this.enviroment.Length - 1; j++)
        {
          float thisScore = this.enviroment[j].score;
          this.enviroment[j].Mutate(Lerp(thisScore, minScore, maxScore, this.minMutationStrength, this.maxMutationStrength));
        }
      }
      Sort();
      return this.enviroment[index];
    }


    /// <summary>
    /// Lerps a value from 1 range to another.
    /// This is used as it maintains a homeomorpic property.
    /// aka x < y => f(x) < f(y)
    /// </summary>
    /// <param name="current">The current value we need to lerp</param>
    /// <param name="minScore">The minimum value of the set</param>
    /// <param name="maxScore">The maximum value of the set</param>
    /// <param name="minStrength">The minimum value a score needs to be altered with</param>
    /// <param name="maxStrength">The maximum value a score can be altered with</param>
    /// <returns>The value with which we're altering the synapses with</returns>
    // TODo(nobel002) implement somthing else than liniar interoplation.
    float Lerp(float current, float minScore, float maxScore, float minStrength, float maxStrength)
    {
      // This is a linear lerp.
      // Calculates how far along the current range it is.
      float amountAlongInput = (current - minScore) / (maxScore - minScore);
      // Map to the correct range.
      return minStrength + amountAlongInput * (maxStrength - minStrength);
    }

  }

  class Net
  {
    public float score { get; set; }
    public int[] shape { get; set; }
    public float[][] nodes { get; set; }
    public float[][][] synapses { get; set; }
    private Random rnd = new Random();

    public Net(int[] shape, float[][] nodes, float[][][] synapses)
    {
      this.shape = shape;
      this.nodes = nodes;
      this.synapses = synapses;
      FillSynapses();
    }
    //TODO.(nobel) write the use function so we can calculate everything.

    /// <summary>
    /// This uses a specific net. Meaning it calculates the weighted sum over the whole net.
    /// </summary>
    /// <param name="inputs">
    /// This is a float[] with the same length as shape[0].
    /// </param>
    /// <returns>
    /// The output array. This will return an array where the numbers of the last nodes are stored. So you can process them however you like.
    /// </returns>
    /// <exception cref="Exception">
    /// This trows an exception if the input array of the net and the provided input array don't match in length.
    /// </exception>
    public float[] Use(float[] inputs)
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
    /// Float[][][] the synapses array you want the net to have.
    /// </param>
    public void SetSynapses(float[][][] synapses)
    {
      //ToDo.(Nobel) check whether the synapses and this.synapses match in shape.
      // The code to check whether the shapes match.
      if (this.synapses.Length != synapses.Length)
        throw new Exception("The shape you provided is not the expected shape.");
      for (int i = 0; i <= synapses.Length - 1; i++)
      {
        for (int j = 0; j <= synapses[i].Length - 1; j++)
        {
          if (this.synapses[i][j].Length != synapses[i][j].Length)
            throw new Exception("The shape you provided is not the expected shape.");
        }
      }

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
    public float[][][] SaveSynapses(string path)
    {
      //TODO.(nobel002) do the file IO but good this time.
      #region Path Stuff
      string path_ = path;
      path_ = path_.Substring(0, path_.Length - 3);
      path_ += '{';
      int prev = this.shape[0];
      int mult = 1;

      for (int i = 1; i <= this.shape.Length - 1; i++)
      {
        if (prev == this.shape[i])
        {
          mult++;
        }
        else
        {
          if (mult > 1)
          {
            path_ += Convert.ToString(prev);
            path_ += 'x';
            path_ += Convert.ToString(mult);
            mult = 1;
          }
          else
          {
            path_ += Convert.ToString(prev);
          }
          if (i < this.shape.Length)
            path_ += ',';
        }
        prev = this.shape[i];

      }
      path_ += Convert.ToString(this.shape[this.shape.Length - 1]);
      path_ += "}.ai";
      #endregion
      Console.WriteLine(path_);
      if (!File.Exists(path_))
      {
        WriteToDisk(path_);
      }
      else
      {
        // Fk it just delete the file and re write it...
        File.Delete(path_);
        WriteToDisk(path_);
        //throw new Exception("File Error, the file you are trying to save  to already exists.");
      }
      return this.synapses;
    }
    /// <summary>
    /// The actual code to write to the disc.
    /// Note this always writes. Only use if you know you can savily write at that location.
    /// </summary>
    /// <param name="path_">the path where to write to.</param>
    void WriteToDisk(string path_)
    {
      String currentDirectory = System.IO.Directory.GetCurrentDirectory();
      Console.WriteLine("-----4 testing ----");
      Console.WriteLine(currentDirectory);
      path_ = currentDirectory + @"\..\..\..\" + path_;
      using (StreamWriter sw = File.CreateText(path_))
      {
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
          if (i < this.synapses.Length - 1)
            sw.Write("\n");
        }
      }
    }

    /// <summary>
    /// This loads a .ai file into a Net instance.
    /// !!Warning!!
    /// This directly modifies the synaps data and their is no way to revert it.
    /// </summary>
    /// <param name="path">The path to the .ai file you want to load.</param>
    /// <returns>It retruns the synapses after their updated</returns>
    public float[][][] LoadSynapses(string path)
    {
      // TODO(nobel002) Check if the size of the provided .ai file is the same as the shape of the existing synaps matrix.
      string[] text = File.ReadAllLines(path);
      for (int i = 0; i < text.Length - 1; i++)
      {
        string[] splittedStrings = text[i].Split('|');
        for (int j = 0; j < splittedStrings.Length - 1; j++)
        {
          string[] individualNumbers = splittedStrings[j].Split(';');
          for (int k = 0; k < individualNumbers[j].Length - 1; k++)
          {
            this.synapses[i][j][k] = Convert.ToSingle(individualNumbers[k]);
          }
        }
      }
      return this.synapses;
    }

    /// <summary>
    /// This is a function which you must override.
    /// I can't write an all in one Score method as it may change depending upon it's use.
    /// This however must be defined in the Net Creator Class.
    /// Note that the Use function should be used here...
    /// </summary>
    /// <param name="score">This is a placeholder</param>
    /// <returns>
    /// The score for this particulair Net. This is then used by the train method to determine its mutaion strength.
    /// </returns>
    public virtual float Score(float score = 0)
    {
      // ToDo(Nobel002) fix this so that it becomes obvious that one must override this particulair function.
      // Expected output - cost
      this.score = score;
      return this.score;
    }

    /// <summary>
    /// This alters the synapses at random. Hence you must determine how mutch.
    /// Implement the back propagation algorithm
    /// </summary>
    /// <param name="mutationStrength">The mutation strength, a mesure of how much the synapses need to be altered.</param>
    public void Mutate(float mutationStrength)
    {
      // ToDo.(nobel) so for each synapsyse calculate the derivative and update the nodes and biases accordingly
      // ToDo (Nobel) so implement the back propagation algorithm
      for (int i = 0; i <= this.synapses.Length - 1; i++)
      {
        for (int j = 0; j <= this.synapses[i].Length - 1; j++)
        {
          for (int k = 0; k <= this.synapses[i][j].Length - 1; k++)
          {
            this.synapses[i][j][k] += mutationStrength * (2 * rnd.NextSingle() - 1);
          }
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
            this.synapses[i][j][k] = rnd.NextSingle();
          }
        }
      }
    }
  }
}