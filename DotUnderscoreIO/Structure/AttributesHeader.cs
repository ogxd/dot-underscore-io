using System.Collections.Generic;
using System.Text;

namespace Ogx {

    public class AttributesHeader : IBinarySerializable {

        public const int SIZE = 34 + 4 + 4 * 8;

        public byte[] unknown0;
        public string signature = "ATTR";
        public uint unknown1 = 999999999;
        public uint logicalFileSize = 3810;
        public uint valuesOffset;
        public uint valuesLen;
        public uint unknown3 = 0;
        public uint unknown4 = 0;
        public uint unknown5 = 0;
        public uint numAttributes;

        public List<Attribute> attributes;

        public AttributesHeader() {
            attributes = new List<Attribute>();
        }

        public AttributesHeader(BitReader reader) {
            Deserialize(reader);
        }

        public override string ToString() {
            return base.ToString() + $"({numAttributes})";
        }

        public void Deserialize(BitReader reader) {
            unknown0 = reader.ReadBytes(34);
            signature = Encoding.ASCII.GetString(reader.ReadBytes(4));
            unknown1 = reader.ReadUInt32();
            logicalFileSize = reader.ReadUInt32();
            valuesOffset = reader.ReadUInt32();
            valuesLen = reader.ReadUInt32();
            unknown3 = reader.ReadUInt32();
            unknown4 = reader.ReadUInt32();
            unknown5 = reader.ReadUInt32();
            numAttributes = reader.ReadUInt32();

            attributes = new List<Attribute>();
            for (int i = 0; i < numAttributes; i++) {
                attributes.Add(new Attribute(reader));
            }
        }

        public void Serialize(BitWriter writer) {

            unknown0 = new byte[34];
            unknown0[10] = 4; // ¯\_(ツ)_/¯

            numAttributes = (uint)attributes.Count;

            valuesOffset = 0;

            for (int i = 0; i < numAttributes; i++) {
                int size = attributes[i].nameLen + 11;
                size += Extensions.GetPadding(size);
                valuesOffset += (uint)size; 
            }

            valuesOffset += (uint)SIZE;
            valuesOffset += (uint)writer.offset;

            BitWriter fork = new BitWriter((int)valuesOffset);

            for (int i = 0; i < numAttributes; i++) {
                attributes[i].SerializeFork(fork);
            }

            valuesLen = (uint)fork.Length;

            writer.Write(unknown0);
            writer.Write(Encoding.ASCII.GetBytes(signature));
            writer.Write(unknown1);
            writer.Write(logicalFileSize);
            writer.Write(valuesOffset);
            writer.Write(valuesLen);
            writer.Write(unknown3);
            writer.Write(unknown4);
            writer.Write(unknown5);
            writer.Write(numAttributes);

            for (int i = 0; i < numAttributes; i++) {
                attributes[i].SerializeHead(writer);
            }

            writer.Write(fork.GetBytes());
        }
    }
}