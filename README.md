# NeuraNet.cs
This is a repo in which i hope to create a library or code base which may help with creating AI in CSharp. I know this isn't the best, not fastest database. But it's simple and I made it as an exersise for me.
## How to use
You'll need the [.NET Core](https://dotnet.microsoft.com/en-us/download) to work with this thing. I think I used .Net 5.0 but i think it'll work with .Net 6.0 (Normally it does)
To use this code you simply need to download id and run the build command.
+ on windows:
  ```$\> dotnet build ```

+ MacOS/ Linux
  Don't know I don't use these operating systems but you'll be able to do it one you have gotten the .NET toolkit etc... (I am not shure whether it works in those operating systems but you'll see.)

## Documentation
You'll first need a `using NeuraNet;` statement to get this working (quite obvious I know). Then you'll need an instance of the `NetCreator class`.
So your hello world would be somthing along the lines of:
```cs
using System;
using NeuraNet;
namespace test
{
  class Program
  {
    static void Main(string[] args)
    {
      
      int[] shape = { 1, 2, 3 };
      const int specimenCount = 10;
      const int trainTime = (int)10e3;
      NetCreator test = new NetCreator(shape, specimenCount, trainTime);
    }
  }
}

```
## TODO
- [ ] Considere wheter to use `System.Double` or `System.Decimal`... This might impact speed and also might affect precision. (As in the decimal system might have a theoretical better precision as the numbers can become more acurate.)
- [ ] so make the used variable a `<T>` variable and pass it as an argument so I can compile three versions: a `float`, `double` and `decimal` one.
- [ ] make a published version.
- [ ] Fix the `.gitignore` file  
- [ ] Check how we can save the nets to dll's or somthing
- [ ] Finish the net class  

+ in the Creator class  
- [ ] Train() { }  
- [ ] Mutate() { }  
- [ ] Sort() { }  
+ in the Net class
- [ ] Check wheter the provided synapses match the synapses shape in the SetSynapses();
- [ ] Normalise the outputs.
- [ ] Load(synapses) { }  
- [ ] Store() { }  