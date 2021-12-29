global using MaterialEditor.Lib;
global using BenchmarkDotNet.Attributes;
global using BenchmarkDotNet.Running;
global using Newtonsoft.Json.Linq;

namespace GrandStarMaterialEditor.Tests;

class Program
{
    static void Main(string[] args)
    {
#if RELEASE
        BenchmarkRunner.Run<Bench>();
#elif DEBUG
        var m = new MaterialFile(new FileInfo("grandstar_materials.json"));
        var color = (Color)m[1].TevColors[0];
        (int, int, int) rbg = ((int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255));
#endif
    }
}