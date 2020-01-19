using System.Text;

namespace Ogx {

    public class DotUnderscore : IBinarySerializable {

        // Header
        public uint magicNumber = 333319;
        public ushort version = 2;
        public ushort reserved = 0;
        public string macOsX = "Mac OS X        ";
        private ushort numEntries { get { return (ushort)entries.Length; } set { entries = new Entry[value]; } }

        public Entry[] entries;

        public DotUnderscore() {
            entries = new Entry[0];
        }

        public DotUnderscore(BitReader reader) {
            Deserialize(reader);
        }

        public void Deserialize(BitReader reader) {
            magicNumber = reader.ReadUInt32();
            version = reader.ReadUInt16();
            reserved = reader.ReadUInt16();
            macOsX = Encoding.ASCII.GetString(reader.ReadBytes(16));
            numEntries = reader.ReadUInt16();

            for (int i = 0; i < entries.Length; i++) {
                entries[i] = new Entry(reader);
            }
        }

        public void Serialize(BitWriter writer) {
            writer.Write(magicNumber);
            writer.Write(version);
            writer.Write(reserved);
            macOsX = macOsX.SetLength(16);
            writer.Write(Encoding.ASCII.GetBytes(macOsX));
            writer.Write(numEntries);

            int forkOffset = writer.Length + entries.Length * Entry.SIZE;
            BitWriter entriesFork = new BitWriter(forkOffset);

            for (int i = 0; i < entries.Length; i++) {
                entries[i].Serialize(writer, entriesFork);
            }

            writer.Write(entriesFork.GetBytes());

            BitWriter footer = new BitWriter(0);

            // Filllllll !
            footer.Write(0x00000000);
            footer.Write(0x01000000);
            footer.Write(0x01000000);
            footer.Write(0x00000000);

            footer.Write(0x001E5468);
            footer.Write(0x69732072);
            footer.Write(0x65736F75);
            footer.Write(0x72636520);

            footer.Write(0x666F726B);
            footer.Write(0x20696E74);
            footer.Write(0x656E7469);
            footer.Write(0x6F6E616C);

            footer.Write(0x6C79206C);
            footer.Write(0x65667420);
            footer.Write(0x626C616E);
            footer.Write(0x6B202020);

            for (int i = 0; i < 12; i++) {
                footer.Write(0x00000000);
                footer.Write(0x00000000);
                footer.Write(0x00000000);
                footer.Write(0x00000000);
            }

            footer.Write(0x00000000);
            footer.Write(0x01000000);
            footer.Write(0x01000000);
            footer.Write(0x00000000);

            footer.Write(0x001E0000);
            footer.Write(0x00000000);
            footer.Write(0x0000001C);
            footer.Write(0x001EFFFF);

            int size = 4096 - writer.Length - footer.Length;

            writer.Write(new byte[size]);
            writer.Write(footer.GetBytes());
        }
    }
}
