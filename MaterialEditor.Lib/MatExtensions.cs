namespace MaterialEditor.Lib;

public static class MatExtensions
{
    public static double EnsureValue(this double num)
    {
        if (num != 0 && num > Color.Max)
            return num / 255;
        return num;
    }
}