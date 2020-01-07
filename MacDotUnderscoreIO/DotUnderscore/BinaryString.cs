using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ogx {

    public class BinaryString : BinaryProperty {

        public string value;
        public int length;

        public BinaryString() {
            
        }

        public BinaryString(BinaryReader2 reader, int halfByte) {
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

            value = Encoding.UTF8.GetString(reader.ReadBytes(length));
        }

        public override void Serialize(BinaryWriter writer) {

        }
    }
}
