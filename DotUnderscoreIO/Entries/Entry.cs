using System;

namespace Ogx {

    public class Entry {

        public uint id;
        public uint offset;
        public uint size;

        public const int SIZE = 4 + 4 + 4;

        public IBinarySerializable data;

        public Entry(uint id) {
            this.id = id;
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
                case 2:
                    break;
                default:
                    Console.WriteLine($"Unknown Entry type id '{id}'");
                    break;
            }

            reader.SetPosition(position);
        }

        public virtual void Serialize(BitWriter writer, BitWriter fork) {

            switch (id)
            {
                case 9:
                    offset = (uint)(fork.offset + fork.Length);
                    data.Serialize(fork);

                    size = (uint)(fork.offset + fork.Length) - offset;
                    size = 3760; // ¯\_(ツ)_/¯
                    break;
                case 2:
                    size = 286;
                    offset = 3810;
                    break;
                default:
                    Console.WriteLine($"Unknown Entry type id '{id}'");
                    break;
            }

            writer.Write(id);
            writer.Write(offset);
            writer.Write(size);
        }
    }
}