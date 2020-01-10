using System.IO;
using System.Text;

namespace Ogx {

    public class BinaryStringUnicode : BinaryProperty {

        public string value;
        public int length;

        public BinaryStringUnicode() {
            
        }

        public BinaryStringUnicode(BinaryParser reader, int halfByte) {
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

            value = Encoding.BigEndianUnicode.GetString(reader.ReadBytes(length));
        }

        public override void Serialize(BinaryWriter writer) {

        }
    }
}
