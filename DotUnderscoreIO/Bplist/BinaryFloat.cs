using System;
using System.IO;

namespace Ogx {

    public class BinaryFloat : BinaryProperty {

        public double value;
        public int length;

        public BinaryFloat() {
            
        }

        public BinaryFloat(BinaryParser reader, int halfByte) {
            length = (int)Math.Pow(2, halfByte);
            Deserialize(reader);
        }

        public override string ToString() {
            return base.ToString() + $" ({length})";
        }

        public override void Deserialize(BinaryParser reader) {
            switch (length) {
                case 32:
                    value = reader.ReadSingle();
                    break;
                case 64:
                    value = reader.ReadDouble();
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
