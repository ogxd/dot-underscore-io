using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ogx {

    public class BinaryInteger : BinaryProperty {

        public long value;
        public int length;

        public BinaryInteger() {
            
        }

        public BinaryInteger(BinaryParser reader, int halfByte) {
            length = (int)Math.Pow(2, halfByte);
            Deserialize(reader);
        }

        public override string ToString() {
            return base.ToString() + $" ({length})";
        }

        public override void Deserialize(BinaryParser reader) {
            Console.WriteLine(length);
            switch (length) {
                case 8:
                    value = reader.ReadByte();
                    break;
                case 16:
                    value = reader.ReadInt16();
                    break;
                case 32:
                    value = reader.ReadInt32();
                    break;
                case 64:
                    value = reader.ReadInt64();
                    break;
                default:
                    Console.WriteLine($"Unsupported integer property length : '{length}'");
                    break;
            }
        }

        public override void Serialize(BinaryWriter writer) {

        }
    }
}
