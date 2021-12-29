namespace MaterialEditor.Lib;

public record struct Material
{
    public string Name { get; set; }

    public int Flag { get; set; }

    public string CullMode { get; set; }

    public bool ZCompLoc { get; set; }

    public bool Dither { get; set; }

    public string[] TextureNames { get; set; }

    public IndTexEntry IndTexEntry { get; set; }

    public Color[] MaterialColors { get; set; }

    public ChannelControl[] ChannelControls { get; set; }

    public Color[] AmbientColors { get; set; }

    public object LightingColors { get; set; }

    public TexCoord1Gen?[] TexCoord1Gens { get; set; }

    public object PostTexCoordGens { get; set; }

    public TexMatrix?[] TexMatrix1 { get; set; }

    public object PostTexMatrix { get; set; }

    public TevOrderEntry?[] TevOrders { get; set; }

    public string[] ColorSels { get; set; }

    public string[] AlphaSels { get; set; }

    public Color?[] TevColors { get; set; }

    public Color[] KonstColors { get; set; }

    public TevStage?[] TevStages { get; set; }

    public SwapMode?[] SwapModes { get; set; }

    public SwapTable?[] SwapTables { get; set; }

    public FogInfo FogInfo { get; set; }

    public double[] RangeAdjustmentTable { get; set; }

    public AlphCompare AlphCompare { get; set; }

    public BMode BMode { get; set; }

    public ZMode ZMode { get; set; }

    public NBTScale NBTScale { get; set; }

    public static implicit operator Material(JObject obj) =>
        obj.ToObject<Material>();

    public static implicit operator JObject(Material mat) =>
        JObject.FromObject(mat);

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}

public record struct ChannelControl(bool Enable, string MaterialSrcColor, string LitMask, string DiffuseFunction, string AttenuationFunction, string AmbientSrcColor);

public record struct TexCoord1Gen(string Type, string Source, string TexMatrixSource);

public record struct TexMatrix(string Projection, int Type, double[] EffectTranslation, double[] Scale, double Rotation, double[] Translation, double[][] ProjectionMatrix);

public record struct TevOrderEntry(string TexCoord, string TexMap, string ChannelId);

public record struct TevStage(string ColorInA, string ColorInB, string ColorInC, string ColorInD, string ColorOp, string ColorBias, string ColorScale, bool ColorClamp, string ColorRegId, string AlphaInA, string AlphaInB, string AlphaInC, string AlphaInD, string AlphaOp, string AlphaBias, string AlphaScale, bool AlphaClamp, string AlphaRegId);

public record struct SwapMode(int RasSel, int TexSel);

public record struct SwapTable(int R, int G, int B, int A);

public record struct FogInfo(int Type, bool Enable, int Center, double StartZ, double EndZ, double NearZ, double FarZ, Color Color, double[] RangeAdjustmentTable);

public record struct AlphCompare(string Comp0, string Reference0, string Operation, string Comp1, string Reference1);

public record struct BMode(string Type, string SourceFact, string DestinationFact, string Operation);

public record struct ZMode(bool Enable, string Function, bool UpdateEnable);

public record struct NBTScale(int Unknown1, double[] Scale);