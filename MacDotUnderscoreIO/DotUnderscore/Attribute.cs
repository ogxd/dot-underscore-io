using System;
using System.IO;
using System.Text;

namespace Ogx {

    public class Attribute : IBinarySerializable {

        public uint ValueOffset;
        public uint ValueLen;
        public ushort Unknown; 
        public byte NameLen;
        public string Name;
        public int padding;
        public object Value;

        public Attribute() {
            Value = new byte[0];
        }

        public Attribute(BinaryParser reader) {
            Deserialize(reader);
        }

        public override string ToString() {
            return Name;
        }

        public void Deserialize(BinaryParser reader) {
            ValueOffset = reader.ReadUInt32();
            ValueLen = reader.ReadUInt32();
            Unknown = reader.ReadUInt16();
            NameLen = reader.ReadByte();
            Name = Encoding.UTF8.GetString(reader.ReadBytes(NameLen));
            padding = reader.Pad();
            int position = reader.GetPosition();
            reader.SetPosition((int)ValueOffset);

            //Value = reader.ReadBytes((int)ValueLen);

            string valueType = Encoding.UTF8.GetString(reader.ReadBytes(6));
            switch (valueType) {
                case "bplist":
                    Value = new BinaryPropertyList(reader);
                    break;
                default:
                    Console.WriteLine($"Unsupported attribute value type : '{valueType}'");
                    break;
            }

            reader.SetPosition(position);

        }

        public void Serialize(BinaryWriter writer) {
            
        }
    }
}
