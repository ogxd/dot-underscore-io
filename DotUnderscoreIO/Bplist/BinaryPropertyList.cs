using System;
using System.Text;

namespace Ogx {

    //null 0000 0000
    //bool 0000 1000 // false
    //bool 0000 1001 17 true
    //fill 0000 1111 // £511 byte
    //int 0001 nnnn // # of bytes is 2*nnnn, big-endian bytes
    //real 0010 nnnn // # of bytes is 2*nnnn, big-endian bytes
    //date 0011 0011 // 8 byte float follows, big-endian bytes
    //data 0100 nnnn // nnnn is number of bytes unless 1111 then int count follows, followed by bytes

    //string 0101 nnnn // ASCII string, nnnn is # of chars, else 1111 then int count, then bytes
    //string 0110 nnnn // Unicode string, nnnn is # of chars, else 1111 then int count, then big-endian 2-byte uint16é_t

    //uid 1000 nnnn 3 // nnnn+1 is # of bytes

    //array 1010 nnnn[int] objref* // nnnn is count, unless '1111', then int count follows

    //set 1100 nnnn[int] objref* // nnnn is count, unless '1111', then int count follows
    //dict 1101 nnnn[int] keyref* objref* // nnnn is count, unless '1111', then int count follows

    public class BinaryPropertyList : IBinarySerializable {

        public string version = "00";
        public BinaryProperty property;

        // Offset Table
        public byte[] offsetTable;

        // Trailer
        public byte offsetTableOffsetSize = 1; // Offset table slot size (1, 2, 4, 8, ...)
        public byte objectRefSize = 1; 
        public long numObjects; // Total number of properties
        public long topObjectOffset;
        public long offsetTableStart;

        public BinaryPropertyList() {
            
        }

        public BinaryPropertyList(BitReader reader) {
            Deserialize(reader);
        }

        public override string ToString() {
            return "bplist" + version;
        }

        public void Deserialize(BitReader reader) {
            version = Encoding.ASCII.GetString(reader.ReadBytes(2));
            switch (version) {
                case "00":
                    property = BinaryProperty.ReadBinaryProperty(reader);
                    break;
                default:
                    Console.WriteLine($"Unsupported binary property list version : {version}");
                    break;
            }

            // Read offset table
            // Read trailer
        }

        public void Serialize(BitWriter writer) {

            BitWriter writerBplist = new BitWriter(0);

            writerBplist.Write(Encoding.ASCII.GetBytes("bplist"));
            writerBplist.Write(Encoding.ASCII.GetBytes(version));

            BitWriter writerOffset = new BitWriter(0); // offset ?

            numObjects = 0;
            property.Serialize(writerBplist, writerOffset, ref numObjects); // When serializing a property, add pointer as well in offset table
            offsetTableStart = writerBplist.Length;

            // Write offset table
            writerBplist.Write(writerOffset);

            // Write trailer
            writerBplist.Write(new byte[6]); // ?
            writerBplist.Write(offsetTableOffsetSize);
            writerBplist.Write(objectRefSize);
            writerBplist.Write(numObjects);
            writerBplist.Write(topObjectOffset);
            writerBplist.Write(offsetTableStart);

            writer.Write(writerBplist);
        }
    }
}