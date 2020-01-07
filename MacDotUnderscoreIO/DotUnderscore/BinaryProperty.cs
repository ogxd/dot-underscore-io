using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ogx {

    public abstract class BinaryProperty : IBinarySerializable {

        public BinaryProperty() {
            
        }

        public BinaryProperty(BinaryReader2 reader) {
            Deserialize(reader);
        }

        public abstract void Deserialize(BinaryReader2 reader);

        public abstract void Serialize(BinaryWriter writer);

        public static BinaryProperty ReadBinaryProperty(BinaryReader2 reader) {
            byte marker = reader.ReadByte();
            int hi = (marker & 0xF0) >> 4;
            int lo = (marker & 0x0F);
            switch (hi) {
                case 0: // Fill (skip)
                    return ReadBinaryProperty(reader);
                case 10: // Array
                    return new BinaryArray(reader, lo);
                case 5: // String
                    return new BinaryString(reader, lo);
                default:
                    Console.WriteLine($"Unsupported binary property type : '{hi}'");
                    return null;
            }
        }
    }
}
