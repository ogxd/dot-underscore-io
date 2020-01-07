using System;
using System.IO;
using System.Text;

namespace Ogx {

    public class Header : IBinarySerializable {

        public uint MagicNumber; // 00 05 16 07
        public ushort Version; // 00 02 
        public ushort Reserved; // 00 00
        public string Macosx; // "Mac OS X        "
        public ushort NumEntries; // 00 02 

        public Header() {

        }

        public Header(BinaryReader2 reader) {
            Deserialize(reader);
        }

        public void Deserialize(BinaryReader2 reader) {
            MagicNumber = reader.ReadUInt32();
            Version = reader.ReadUInt16();
            Reserved = reader.ReadUInt16();
            Macosx = Encoding.UTF8.GetString(reader.ReadBytes(16));
            NumEntries = reader.ReadUInt16();
        }

        public void Serialize(BinaryWriter writer) {
            
        }
    }
}
