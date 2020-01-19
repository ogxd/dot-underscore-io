using System.Collections.Generic;
using System.IO;

namespace Ogx {

    public class BinaryArray : BinaryProperty {

        public BinaryProperty[] properties = new BinaryProperty[0];
        private int length { get { return properties.Length; } set { properties = new BinaryProperty[value]; } }

        public BinaryArray() {
            
        }

        public BinaryArray(BitReader reader, int halfByte) {
            if (halfByte == 15) {
                halfByte = reader.ReadInt32();
            }
            length = halfByte;
            Deserialize(reader);
        }

        public override string ToString() {
            return base.ToString() + $" ({length})";
        }

        public override void Deserialize(BitReader reader) {
            for (int i = 0; i < length; i++) {
                properties[i] = ReadBinaryProperty(reader);
                //Console.WriteLine(Convert.ToString(b2, 2).PadLeft(8, '0'));
            }
        }

        public override void Serialize(BitWriter writer, BitWriter offsetWriter, ref long numProperty) {
            numProperty++;
            offsetWriter.Write((byte)writer.Length);
            WriteMarker(writer, 0x0A, length);
            for (int p = 0; p < length; p++) {
                writer.Write((byte)(p + 1));
            }
            foreach (var property in properties) {
                property.Serialize(writer, offsetWriter, ref numProperty);
            }
        }
    }
}