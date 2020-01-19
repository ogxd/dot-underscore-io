using System;
using System.IO;

namespace Ogx {

    public static class BinaryHelper {

        public static TBinarySerializable Read<TBinarySerializable>(string file) where TBinarySerializable : IBinarySerializable {
            return Read<TBinarySerializable>(File.ReadAllBytes(file));
        }

        public static TBinarySerializable Read<TBinarySerializable>(byte[] bytes) where TBinarySerializable : IBinarySerializable {
            TBinarySerializable binaryObject = Activator.CreateInstance<TBinarySerializable>();
            BitReader br = new BitReader(bytes);
            binaryObject.Deserialize(br);
            return binaryObject;
        }

        public static void Write(IBinarySerializable binaryObject, string file) {
            File.WriteAllBytes(file, Write(binaryObject));
        }

        public static byte[] Write(IBinarySerializable binaryObject) {
            BitWriter br = new BitWriter(0);
            binaryObject.Serialize(br);
            byte[] bytes = br.GetBytes().ToArray();
            return bytes;
        }
    }
}
