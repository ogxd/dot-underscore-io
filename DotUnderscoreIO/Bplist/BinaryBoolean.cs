using System.IO;

namespace Ogx {

    public class BinaryBoolean : BinaryProperty {

        public bool value;
        private int halfByte;

        public BinaryBoolean() {
            
        }

        public BinaryBoolean(BinaryParser reader, int halfByte) {
            this.halfByte = halfByte;
            Deserialize(reader);
        }

        public override void Deserialize(BinaryParser reader) {
            value = halfByte == 9;
        }

        public override void Serialize(BinaryWriter writer) {

        }
    }
}
