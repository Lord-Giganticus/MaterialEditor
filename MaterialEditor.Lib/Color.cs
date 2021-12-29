namespace MaterialEditor.Lib
{
    public record struct Color
    {
        public double R { get; set; }

        public double G { get; set; }

        public double B { get; set; }

        public double A { get; set; }

        public const double Max = 1D;

        public Color((double R, double G, double B) tup)
        {
            R = tup.R; G = tup.G; B = tup.B; A = 1;
            EnsureValues();
        }

        public Color((double R, double G, double B, double A) tup)
        {
            R = tup.R; G = tup.G; B= tup.B; A = tup.A;
            EnsureValues();
        }

        public Color(double R, double G, double B) : this((R, G, B)) { }

        public Color(double R, double G, double B, double A) : this((R, G, B, A)) { }

        public void Deconstruct(out double R, out double G, out double B, out double A)
        {
            R = (this.R * 255);
            G = (this.G * 255);
            B = (this.B * 255);
            A = (this.A * 255);
        }

        public static implicit operator (double R, double G, double B, double A)(Color color)
        {
            var (r, g, b, a) = color;
            return (r, g, b, a);
        }

        public static implicit operator Color((double R, double G, double B, double A) tup) =>
            new(tup);

        private void EnsureValues()
        {
            if (R != 0 && R > Max) R /= 255;
            if (G != 0 && G > Max) G /= 255;
            if (B != 0 && B > Max) B /= 255;
            if (A != 0 && A > Max) A /= 255;
        }

        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
