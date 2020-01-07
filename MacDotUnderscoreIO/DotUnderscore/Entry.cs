using System.IO;

namespace Ogx {

    public class Entry : IBinarySerializable {

        public uint Id;
        public uint Offset;
        public uint Size;

        public Entry() {

        }

        public Entry(BinaryReader2 reader) {
            Deserialize(reader);
        }

        public void Deserialize(BinaryReader2 reader) {
            Id = reader.ReadUInt32();
            Offset = reader.ReadUInt32();
            Size = reader.ReadUInt32();
        }

        public void Serialize(BinaryWriter writer) {
            
        }
    }
}
