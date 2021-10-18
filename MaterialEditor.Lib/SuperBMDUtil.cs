using SuperBMDLib;
using SuperBMDLib.Geometry;
using System.IO;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GameFormatReader.Common;

namespace MaterialEditor.Lib
{
    public class SuperBMDUtil
    {
        public static Model GetModel(FileInfo file)
        {
            using FileStream str = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            using var reader = new EndianBinaryReader(str, Endian.Big);
            var dir = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Directory;
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

        public static MaterialFile GetMaterials(Model mod) => new MaterialFile(JsonConvert.SerializeObject(mod.Materials.m_Materials, Formatting.Indented));

        public static (TexHeader[] Headers, JArray Array) GetTexHeaders(Model mod) => TexHeader.GetTexHeaders(JsonConvert.SerializeObject(mod.Textures.Textures, Formatting.Indented));

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
