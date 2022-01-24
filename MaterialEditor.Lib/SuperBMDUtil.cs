namespace MaterialEditor.Lib
{
    public static class SuperBMDUtil
    {
        public static Model GetModel(FileInfo file)
        {
            using FileStream str = new(file.FullName, FileMode.Open, FileAccess.Read);
            using var reader = new EndianBinaryReader(str, Endian.Big);
            var dir = new FileInfo(Environment.ProcessPath).Directory;
            var args = new Arguments
            {
                input_path = dir.FullName
            };
            var r = new Model(reader, args);
            foreach (var f in dir.GetFiles())
                if (f.Name.Contains("materials.json"))
                    f.Delete();
            return r;
        }

        public static (MaterialFile Mats, TexHeaderFile TexHeader) LoadModel(FileInfo file)
        {
            using FileStream str = new(file.FullName, FileMode.Open, FileAccess.Read);
            using var reader = new EndianBinaryReader(str, Endian.Big);
            reader.Skip(32);
            var inf = new INF1(reader, 32);
            var vtx = new VTX1(reader, (int)reader.BaseStream.Position);
            var evp = new EVP1(reader, (int)reader.BaseStream.Position);
            var drw = new DRW1(reader, (int)reader.BaseStream.Position);
            var jnt = new JNT1(reader, (int)reader.BaseStream.Position);
            var shp = SHP1.Create(reader, (int)reader.BaseStream.Position);
            var mat = new MAT3(reader, (int)reader.BaseStream.Position);
            SkipMDL3(reader);
            var tex = new TEX1(reader, (int)reader.BaseStream.Position);
            return (mat, tex);
        }

        private static void SkipMDL3(EndianBinaryReader reader)
        {
            if (reader.PeekReadInt32() == 0x4D444C33)
            {
                int mdl3Size = reader.ReadInt32At(reader.BaseStream.Position + 4);
                reader.Skip(mdl3Size);
            }
        }

        public static MemoryStream ToStream(Model mod, bool isBDL = false)
        {
            var ms = new MemoryStream();
            var writer = new EndianBinaryWriter(ms, Endian.Big);
            if (isBDL)
                writer.Write("J3D2bdl4".ToCharArray());
            else
                writer.Write("J3D2bmd3".ToCharArray());
            writer.Write(0);

            if (isBDL)
                writer.Write(9);
            else
                writer.Write(8);
            int packetcount = 0;
            int vertexcount;
            foreach (Shape shape in mod.Shapes.Shapes)
                packetcount += shape.Packets.Count;
            vertexcount = mod.VertexData.Attributes.Positions.Count;
            mod.Scenegraph.Write(writer, packetcount, vertexcount);
            mod.VertexData.Write(writer);
            mod.SkinningEnvelopes.Write(writer);
            mod.PartialWeightData.Write(writer);
            mod.Joints.Write(writer);
            mod.Shapes.Write(writer);
            mod.Materials.Write(writer);
            if (isBDL)
                mod.MatDisplayList.Write(writer);
            mod.Textures.Write(writer);
            writer.Seek(8, SeekOrigin.Begin);
            writer.Write((int)writer.BaseStream.Length);
            writer.Flush();
            return ms;
        }
    }
}
