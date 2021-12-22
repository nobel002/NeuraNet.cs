# NeuraNet.cs
This is a repo in which i hope to create a library or code base which may help with creating AI in CSharp. I know this isn't the best, not fastest database. But it's simple and I made it as an exersise for me.
## How to use
You'll need the [.NET Core](https://dotnet.microsoft.com/en-us/download) to work with this thing. To use this code you simply need to download id and run the build command.
I know it works with .NET 6.0 and .NET 5.0 for other versions you'll need to figure it out.
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

## The `.ai` format
I am using this format to save the synaps data of a neural network. It is a format to store 3D arrays and number data. 
| Level        | Seperator    |
| ------------ | ------------ |
| Top Level    | `a new line` |
| Middel Level | `\|`         |
| Lowest Level | `;`          |

This means that the inividual numbers in an aray are stored like: `0,123;1000,000;etc`. Then we have the `|` to seperate the arrays and this is followed by the `new lines`. so this would look like this:
```
123,123;1000,100|0,00,1;1;
123,123;1000,100|0,00,1;1;|999;100,1000
100,00;0,1001| 0,89796564
```
`NOTE`
In contradiction to what you'd expect is the comma used as the decimal point. This happend due to c#'s way of printing numbers to files.


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


# Licence

<a rel="license" href="http://creativecommons.org/licenses/by/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by/4.0/88x31.png" /></a><br /><span xmlns:dct="http://purl.org/dc/terms/" href="http://purl.org/dc/dcmitype/Text" property="dct:title" rel="dct:type">NeuraNet.cs</span> by <a xmlns:cc="http://creativecommons.org/ns#" href="https://github.com/nobel002" property="cc:attributionName" rel="cc:attributionURL">Nobel002</a> is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by/4.0/">Creative Commons Attribution 4.0 International License</a>.<br />Based on a work at <a xmlns:dct="http://purl.org/dc/terms/" href="https://github.com/nobel002/NeuraNet.cs" rel="dct:source">https://github.com/nobel002/NeuraNet.cs</a>.