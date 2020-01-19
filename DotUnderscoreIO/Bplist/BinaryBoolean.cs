using System.IO;

namespace Ogx {

    public class BinaryBoolean : BinaryProperty {

        public bool value;
        private int halfByte;

        public BinaryBoolean() {
            
        }

        public BinaryBoolean(BitReader reader, int halfByte) {
            this.halfByte = halfByte;
            Deserialize(reader);
        }

        public override void Deserialize(BitReader reader) {
            value = halfByte == 9;
        }

        public override void Serialize(BitWriter writer, BitWriter offsetWriter, ref long numProperty) {

        }
    }
}