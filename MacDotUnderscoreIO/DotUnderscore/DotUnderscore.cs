using System.IO;

namespace Ogx {

    public class DotUnderscore : IBinarySerializable {

        public Header header;
        public Entry entry1;
        public Entry entry2;
        public byte[] unknown;
        public Attributes attributes;

        public DotUnderscore() {
            header = new Header();
            entry1 = new Entry();
            entry2 = new Entry();
            unknown = new byte[34];
            attributes = new Attributes();
        }

        public DotUnderscore(BinaryReader2 reader) {
            Deserialize(reader);
        }

        public void Deserialize(BinaryReader2 reader) {
            header = new Header(reader);
            entry1 = new Entry(reader);
            entry2 = new Entry(reader);
            unknown = reader.ReadBytes(34);
            attributes = new Attributes(reader);
        }

        public void Serialize(BinaryWriter writer) {
            
        }
    }
}
