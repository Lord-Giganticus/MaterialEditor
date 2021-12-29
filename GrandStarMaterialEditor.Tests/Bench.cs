namespace GrandStarMaterialEditor.Tests;

public class Bench
{
    [Benchmark]
    public string LoadFromModel()
    {
        var tup = SuperBMDUtil.LoadModel(new FileInfo("GrandStar.bdl"));
        var m = tup.Mats;
        m.TevColors[1][0] = new Color(88, 29, 144);
        return m.ToString();
    }
    [Benchmark]
    public string LoadFromJson()
    {
        var m = new MaterialFile(new FileInfo("grandstar_materials.json"));
        m.TevColors[1][0] = new Color(88, 29, 144);
        return m.ToString();
    }
}