using System;
using System.IO;

namespace Ogx {

    public abstract class BinaryProperty : IBinarySerializable {

        public BinaryProperty() {
            
        }

        public BinaryProperty(BinaryParser reader) {
            Deserialize(reader);
        }

        public abstract void Deserialize(BinaryParser reader);

        public abstract void Serialize(BinaryWriter writer);

        public static BinaryProperty ReadBinaryProperty(BinaryParser reader) {
            byte marker = reader.ReadByte();
            int hi = (marker & 0xF0) >> 4;
            int lo = (marker & 0x0F);
            switch (hi) {
                case 0:
                    switch (lo) {
                        case 8:
                        case 9: // Boolean
                            return new BinaryBoolean(reader, lo);
                        default: // Fill (skip)
                            return ReadBinaryProperty(reader);
                    }
                case 1: // Integer
                    return new BinaryInteger(reader, lo);
                case 2: // Float
                    return new BinaryFloat(reader, lo);
                case 4: // Blob
                    return new BinaryBlob(reader, lo);
                case 5: // ASCII String
                    return new BinaryStringASCII(reader, lo);
                case 6: // Unicode String
                    return new BinaryStringUnicode(reader, lo);
                case 7:
                case 9:
                case 11:
                case 14:
                case 15: // Unused (skip)
                    return ReadBinaryProperty(reader);
                case 10: // Array
                    return new BinaryArray(reader, lo);
                default: // Skip
                    Console.WriteLine($"Unsupported binary property type : '{hi}'");
                    return ReadBinaryProperty(reader);
            }
        }
    }
}
