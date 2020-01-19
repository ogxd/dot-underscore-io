using System.Text;

namespace Ogx {

    public class BinaryStringASCII : BinaryProperty {

        public string value;
        public int length;

        public BinaryStringASCII() {
            
        }

        public BinaryStringASCII(BitReader reader, int halfByte) {
            length = halfByte;
            Deserialize(reader);
        }

        public override string ToString() {
            return base.ToString() + $" ({length})";
        }

        public override void Deserialize(BitReader reader) {
            if (length == 15) {
                length = reader.ReadInt32();
            }

            value = Encoding.ASCII.GetString(reader.ReadBytes(length));
        }

        public override void Serialize(BitWriter writer, BitWriter offsetWriter, ref long numProperty) {
            numProperty++;
            offsetWriter.Write((byte)writer.Length);
            length = value.Length;
            WriteMarker(writer, 0x05, length);
            writer.Write(Encoding.ASCII.GetBytes(value));
        }
    }
}