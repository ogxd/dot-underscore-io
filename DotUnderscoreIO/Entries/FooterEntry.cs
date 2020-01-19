using System;

namespace Ogx {

    public class FooterEntry : Entry {

        public FooterEntry() {

        }

        public FooterEntry(BitReader reader) {
            Deserialize(reader);
        }

        public override void Deserialize(BitReader reader) {
            id = reader.ReadUInt32();
            offset = reader.ReadUInt32();
            size = reader.ReadUInt32();
        }

        public override void Serialize(BitWriter writer, BitWriter fork) {

            writer.Write(id);
            writer.Write(offset);
            writer.Write(size);
        }
    }
}