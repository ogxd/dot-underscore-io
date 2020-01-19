#define BIG_ENDIAN

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Ogx {

    public class BitWriter {

        public readonly int offset;

        private List<byte> bytes;
        private int position;

        public int Length => bytes.Count;

        public List<byte> GetBytes() {
            return bytes;
        }

        public BitWriter(int offset) {
            this.offset = offset;
            this.bytes = new List<byte>(1024);
        }

        public byte ReadByte() {
            return bytes[position++];
        }

        public void Write(sbyte value) {
            IEnumerable<byte> data = BitConverter.GetBytes(value);
#if BIG_ENDIAN
            data = data.Reverse();
#endif
            bytes.AddRange(data);
        }

        public void Write(ushort value) {
            IEnumerable<byte> data = BitConverter.GetBytes(value);
#if BIG_ENDIAN
            data = data.Reverse();
#endif
            bytes.AddRange(data);
        }

        public void Write(uint value) {
            IEnumerable<byte> data = BitConverter.GetBytes(value);
#if BIG_ENDIAN
            data = data.Reverse();
#endif
            bytes.AddRange(data);
        }

        public void Write(ulong value) {
            IEnumerable<byte> data = BitConverter.GetBytes(value);
#if BIG_ENDIAN
            data = data.Reverse();
#endif
            bytes.AddRange(data);
        }

        public void Write(short value) {
            IEnumerable<byte> data = BitConverter.GetBytes(value);
#if BIG_ENDIAN
            data = data.Reverse();
#endif
            bytes.AddRange(data);
        }

        public void Write(int value) {
            IEnumerable<byte> data = BitConverter.GetBytes(value);
#if BIG_ENDIAN
            data = data.Reverse();
#endif
            bytes.AddRange(data);
        }

        public void Write(long value) {
            IEnumerable<byte> data = BitConverter.GetBytes(value);
#if BIG_ENDIAN
            data = data.Reverse();
#endif
            bytes.AddRange(data);
        }

        public void Write(float value) {
            IEnumerable<byte> data = BitConverter.GetBytes(value);
#if BIG_ENDIAN
            data = data.Reverse();
#endif
            bytes.AddRange(data);
        }

        public void Write(double value) {
            IEnumerable<byte> data = BitConverter.GetBytes(value);
#if BIG_ENDIAN
            data = data.Reverse();
#endif
            bytes.AddRange(data);
        }

        public void Write(byte value) {
            bytes.Add(value);
        }

        public void Write(IEnumerable<byte> value) {
            bytes.AddRange(value);
        }

        public void Write(BitWriter writer) {
            bytes.AddRange(writer.bytes);
        }

        public void Write(sbyte[] value) {
            ReadOnlySpan<sbyte> data = new ReadOnlySpan<sbyte>(value);
            ReadOnlySpan<byte> sbuffer = MemoryMarshal.Cast<sbyte, byte>(data);
            bytes.AddRange(sbuffer.ToArray());
        }

        public int Pad() {
            int padding = 0;
            if ((Length + offset) % 4 > 0)
                padding = 4 - (Length + offset) % 4;
            Write(new byte[padding]);
            return padding;
        }
    }
}
