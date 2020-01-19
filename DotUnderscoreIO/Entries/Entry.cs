using System;

namespace Ogx {

    public class Entry {

        public uint id = 9;
        public uint offset;
        public uint size;

        public const int SIZE = 4 + 4 + 4;

        public IBinarySerializable data;

        public Entry() {

        }

        public Entry(BitReader reader) {
            Deserialize(reader);
        }

        public virtual void Deserialize(BitReader reader) {
            id = reader.ReadUInt32();
            offset = reader.ReadUInt32();
            size = reader.ReadUInt32();

            var position = reader.GetPosition();
            reader.SetPosition((int)offset);

            switch (id) {
                case 9:
                    data = new AttributesHeader(reader);
                    break;
                default:
                case 2:
                    Console.WriteLine($"Unknown Entry type id '{id}'");
                    break;
            }

            reader.SetPosition(position);
        }

        public virtual void Serialize(BitWriter writer, BitWriter fork) {

            offset = (uint)(fork.offset + fork.Length);

            data.Serialize(fork);

            size = (uint)(fork.offset + fork.Length) - offset;
            size = 3760; // ¯\_(ツ)_/¯

            writer.Write(id);
            writer.Write(offset);
            writer.Write(size);
        }
    }
}