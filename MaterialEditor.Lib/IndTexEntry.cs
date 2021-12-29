namespace MaterialEditor.Lib;

public record struct IndTexEntry
{
    public bool HasLookup { get; set; }

    public int IndTexStageNum { get; set; }

    public TevOrder[] TevOrders { get; set; }

    public Matrice[] Matrices { get; set; }

    public Scale[] Scales { get; set; }

    public TevStageEntry[] TevStages { get; set; }
}

public record struct TevOrder(string TexCoord, string TexMap);

public record struct Matrice(int[][] Matrix, int Exponent);

public record struct Scale(string ScaleS, string ScaleT);

public record struct TevStageEntry
{
    public string TevStage { get; set; }

    public string IndTexFormat { get; set; }

    public string IndTexBiasSel { get; set; }

    public string IndTexMtxId { get; set; }

    public string IndTexWrapS { get; set; }

    public string IndTexWrapT { get; set; }

    public bool AddPrev { get; set; }

    public bool UtcLod { get; set; }

    public string AlphaSel { get; set; }
}