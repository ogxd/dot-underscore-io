using System;
using System.IO;
using System.Text;

namespace Ogx {

    public class Attributes : IBinarySerializable {

        public string Signature; // "ATTR"
        public uint Unknown1;
        public uint LogicalFileSize;
        public uint ValuesOffset;
        public uint ValuesLen;
        public uint Unknown3; // 0
        public uint Unknown4; // 0
        public uint Unknown5; // 0
        public uint NumAttributes;

        public Attribute[] attributes;

        public Attributes() {
            attributes = new Attribute[0];
        }

        public Attributes(BinaryReader2 reader) {
            Deserialize(reader);
        }

        public override string ToString() {
            return base.ToString() + $"({NumAttributes})";
        }

        public void Deserialize(BinaryReader2 reader) {
            Signature = Encoding.UTF8.GetString(reader.ReadBytes(4));
            Unknown1 = reader.ReadUInt32();
            LogicalFileSize = reader.ReadUInt32();
            ValuesOffset = reader.ReadUInt32();
            ValuesLen = reader.ReadUInt32();
            Unknown3 = reader.ReadUInt32();
            Unknown4 = reader.ReadUInt32();
            Unknown5 = reader.ReadUInt32();
            NumAttributes = reader.ReadUInt32();

            attributes = new Attribute[NumAttributes];
            for (int i = 0; i < NumAttributes; i++) {
                attributes[i] = new Attribute(reader);
            }
        }

        public void Serialize(BinaryWriter writer) {
            
        }
    }
}
