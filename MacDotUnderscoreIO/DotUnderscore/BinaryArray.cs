using System.Collections.Generic;
using System.IO;

namespace Ogx {

    public class BinaryArray : BinaryProperty {

        public List<BinaryProperty> properties = new List<BinaryProperty>();
        int length;

        public BinaryArray() {
            
        }

        public BinaryArray(BinaryParser reader, int halfByte) {
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

            for (int i = 0; i < length; i++) {
                properties.Add(BinaryProperty.ReadBinaryProperty(reader));
                //Console.WriteLine(Convert.ToString(b2, 2).PadLeft(8, '0'));
            }
        }

        public override void Serialize(BinaryWriter writer) {

        }
    }
}
