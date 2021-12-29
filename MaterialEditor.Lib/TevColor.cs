namespace MaterialEditor.Lib
{
    public struct TevColor
    {
        public double R { get; set; }

        public double G { get; set; }

        public double B { get; set; }

        public double A { get; set; }

        public TevColor((double R, double G, double B) tup)
        {
            R = tup.R; G = tup.G; B = tup.B; A = 1;
            EnsureValues();
        }

        public TevColor(double R, double G, double B) : this((R, G, B)) { }

        private void EnsureValues()
        {
            if (R != 0 && R >= 1) R /= 255;
            if (G != 0 && G >= 1) G /= 255;
            if (B != 0 && B > 1) B /= 255;
        }

        public override string ToString() => JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
