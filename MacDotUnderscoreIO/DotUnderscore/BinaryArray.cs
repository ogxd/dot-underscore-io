using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ogx {

    public class BinaryArray : BinaryProperty {

        public List<BinaryProperty> properties = new List<BinaryProperty>();
        int length;

        public BinaryArray() {
            
        }

        public BinaryArray(BinaryReader2 reader, int halfByte) {
            length = halfByte;
            Deserialize(reader);
        }

        public override string ToString() {
            return base.ToString() + $" ({length})";
        }

        public override void Deserialize(BinaryReader2 reader) {
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
