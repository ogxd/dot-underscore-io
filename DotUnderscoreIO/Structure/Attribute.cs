 using System;
using System.Text;

namespace Ogx {

    public class Attribute {

        public uint valueOffset;
        public uint valueLen;
        public ushort unknown;
        private byte _nameLen;
        public byte nameLen { get { return (byte)name.Length; } set { _nameLen = value; } }
        public string name;
        public int padding;
        public IBinarySerializable value;

        public Attribute() {

        }

        public Attribute(BitReader reader) {
            Deserialize(reader);
        }

        public override string ToString() {
            return name;
        }

        public void Deserialize(BitReader reader) {
            valueOffset = reader.ReadUInt32();
            valueLen = reader.ReadUInt32();
            unknown = reader.ReadUInt16();
            _nameLen = reader.ReadByte();
            name = Encoding.ASCII.GetString(reader.ReadBytes(_nameLen));
            padding = reader.Pad();
            int position = reader.GetPosition();
            reader.SetPosition((int)valueOffset);

            string valueType = Encoding.ASCII.GetString(reader.ReadBytes(6));
            switch (valueType) {
                case "bplist":
                    value = new BinaryPropertyList(reader);
                    break;
                default:
                    Console.WriteLine($"Unsupported attribute value type : '{valueType}'");
                    break;
            }

            reader.SetPosition(position);
        }

        public void SerializeHead(BitWriter writer) {

            // Write in Attribute table
            writer.Write(valueOffset);
            writer.Write(valueLen);
            writer.Write(unknown);
            writer.Write(nameLen);
            writer.Write(Encoding.ASCII.GetBytes(name));

            padding = writer.Pad();
        }

        public void SerializeFork(BitWriter fork) {

            valueOffset = (uint)(fork.offset + fork.Length);

            value.Serialize(fork);

            valueLen = (uint)(fork.offset + fork.Length) - valueOffset;

            nameLen = (byte)name.Length;
        }
    }
}
