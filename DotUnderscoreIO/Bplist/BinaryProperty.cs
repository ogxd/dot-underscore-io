using System;

namespace Ogx {

    public abstract class BinaryProperty {

        public BinaryProperty() {
            
        }

        public BinaryProperty(BitReader reader) {
            Deserialize(reader);
        }

        public abstract void Deserialize(BitReader reader);

        public abstract void Serialize(BitWriter writer, BitWriter offsetWriter, ref long numProperty);

        public static BinaryProperty ReadBinaryProperty(BitReader reader) {
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

        public static void WriteMarker(BitWriter writer, byte marker, int length) {
            if (length < 15) {
                writer.Write((byte)(((marker & 0x0F) << 4) | (length & 0x0F)));
            } else {
                writer.Write((byte)(((marker & 0x0F) << 4) | 0x0F));
                writer.Write(length);
            }
        }
    }
}