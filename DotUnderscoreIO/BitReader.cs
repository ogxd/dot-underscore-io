#define BIG_ENDIAN

using System;
using System.Runtime.InteropServices;

namespace Ogx {

    public class BitReader {

        private byte[] bytes;
        private int position;

        public BitReader(byte[] bytes) {
            this.bytes = bytes;
        }

        public byte ReadByte() {
            return bytes[position++];
        }

        public unsafe sbyte ReadSByte() {
            byte b = bytes[position++];
            return *((sbyte*)&b);
        }

        public ushort ReadUInt16() {
            ReadOnlySpan<byte> data = new ReadOnlySpan<byte>(bytes, position, 2);
            position += 2;
#if BIG_ENDIAN
            return (ushort)(data[1] | data[0] << 8);
#else
            return (ushort)(data[0] | data[1] << 8);
#endif
        }

        public uint ReadUInt32() {
            ReadOnlySpan<byte> data = new ReadOnlySpan<byte>(bytes, position, 4);
            position += 4;
#if BIG_ENDIAN
            return (uint)(data[3] | data[2] << 8 | data[1] << 16 | data[0] << 24);
#else
            return (uint)(data[0] | data[1] << 8 | data[2] << 16 | data[3] << 24);
#endif
        }

        public ulong ReadUInt64() {
            ReadOnlySpan<byte> data = new ReadOnlySpan<byte>(bytes, position, 8);
            position += 8;
#if BIG_ENDIAN
            uint lo = (uint)(data[3] | data[2] << 8 | data[1] << 16 | data[0] << 24);
            uint hi = (uint)(data[7] | data[6] << 8 | data[5] << 16 | data[4] << 24);
            return ((ulong)lo) << 32 | hi;
#else
            uint lo = (uint)(data[0] | data[1] << 8 | data[2] << 16 | data[3] << 24);
            uint hi = (uint)(data[4] | data[5] << 8 | data[6] << 16 | data[7] << 24);
            return ((ulong)hi) << 32 | lo;
#endif
        }

        public short ReadInt16() {
            ReadOnlySpan<byte> data = new ReadOnlySpan<byte>(bytes, position, 2);
            position += 2;
#if BIG_ENDIAN
            return (short)(data[1] | data[0] << 8);
#else
            return (short)(data[0] | data[1] << 8);
#endif
        }

        public int ReadInt32() {
            ReadOnlySpan<byte> data = new ReadOnlySpan<byte>(bytes, position, 4);
            position += 4;
#if BIG_ENDIAN
            return data[3] | data[2] << 8 | data[1] << 16 | data[0] << 24;
#else
            return data[0] | data[1] << 8 | data[2] << 16 | data[3] << 24;
#endif
        }

        public long ReadInt64() {
            ReadOnlySpan<byte> data = new ReadOnlySpan<byte>(bytes, position, 8);
            position += 8;
#if BIG_ENDIAN
            uint lo = (uint)(data[3] | data[2] << 8 | data[1] << 16 | data[0] << 24);
            uint hi = (uint)(data[7] | data[6] << 8 | data[5] << 16 | data[4] << 24);
            return ((long)lo) << 32 | hi;
#else
            uint lo = (uint)(data[0] | data[1] << 8 | data[2] << 16 | data[3] << 24);
            uint hi = (uint)(data[4] | data[5] << 8 | data[6] << 16 | data[7] << 24);
            return ((long)hi) << 32 | lo;
#endif
        }

        public unsafe float ReadSingle() {
            ReadOnlySpan<byte> data = new ReadOnlySpan<byte>(bytes, position, 4);
            position += 4;
#if BIG_ENDIAN
            uint tmpBuffer = (uint)(data[3] | data[2] << 8 | data[1] << 16 | data[0] << 24);
#else
            uint tmpBuffer = (uint)(data[0] | data[1] << 8 | data[2] << 16 | data[3] << 24);
#endif
            return *((float*)&tmpBuffer);
        }

        public unsafe double ReadDouble() {
            ReadOnlySpan<byte> data = new ReadOnlySpan<byte>(bytes, position, 8);
            position += 8;
#if BIG_ENDIAN
            uint lo = (uint)(data[3] | data[2] << 8 | data[1] << 16 | data[0] << 24);
            uint hi = (uint)(data[7] | data[6] << 8 | data[5] << 16 | data[4] << 24);
            ulong tmpBuffer = ((ulong)lo) << 32 | hi;
#else
            uint lo = (uint)(data[0] | data[1] << 8 | data[2] << 16 | data[3] << 24);
            uint hi = (uint)(data[4] | data[5] << 8 | data[6] << 16 | data[7] << 24);
            ulong tmpBuffer = ((ulong)hi) << 32 | lo;
#endif
            return *((double*)&tmpBuffer);
        }

        public byte[] ReadBytes(int count) {
            ReadOnlySpan<byte> data = new ReadOnlySpan<byte>(bytes, position, count);
            position += count;
            return data.ToArray();
        }

        public sbyte[] ReadSBytes(int count) {
            ReadOnlySpan<byte> data = new ReadOnlySpan<byte>(bytes, position, count);
            position += count;
            ReadOnlySpan<sbyte> sbuffer = MemoryMarshal.Cast<byte, sbyte>(data);
            return sbuffer.ToArray();
        }

        public int Pad() {
            int padding = 0;
            if (position % 4 > 0)
                padding = 4 - position % 4;
            position += padding;
            return padding;
        }

        public void SetPosition(int position) {
            this.position = position;
        }

        public int GetPosition() {
            return position;
        }
    }
}
