using System.IO;

namespace Ogx {

    public class BinaryBlob : BinaryProperty {

        public byte[] value;
        public int length;

        public BinaryBlob() {
            
        }

        public BinaryBlob(BinaryParser reader, int halfByte) {
            length = halfByte;
            Deserialize(reader);
        }

        public override string ToString() {
            return base.ToString() + $" ({length})";
        }

        public override void Deserialize(BinaryParser reader) {
            if (length == 15) {
                length = reader.ReadInt32();
            }

            value = reader.ReadBytes(length);
        }

        public override void Serialize(BinaryWriter writer) {

        }
    }
}
