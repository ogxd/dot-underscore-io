using System.IO;
using System.Text;

namespace Ogx {

    public class BinaryStringASCII : BinaryProperty {

        public string value;
        public int length;

        public BinaryStringASCII() {
            
        }

        public BinaryStringASCII(BinaryParser reader, int halfByte) {
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

            value = Encoding.UTF8.GetString(reader.ReadBytes(length));
        }

        public override void Serialize(BinaryWriter writer) {

        }
    }
}
