using System;
using System.IO;

namespace Ogx {

    public static class BinaryHelper {

        public static TBinarySerializable Read<TBinarySerializable>(string file) where TBinarySerializable : IBinarySerializable {
            return Read<TBinarySerializable>(File.ReadAllBytes(file));
        }

        public static TBinarySerializable Read<TBinarySerializable>(byte[] bytes) where TBinarySerializable : IBinarySerializable {
            TBinarySerializable binaryObject = Activator.CreateInstance<TBinarySerializable>();
            BinaryParser br = new BinaryParser(bytes);
            binaryObject.Deserialize(br);
            return binaryObject;
        }

        public static void Write(IBinarySerializable binaryObject, string file) {
            File.WriteAllBytes(file, Write(binaryObject));
        }

        public static byte[] Write(IBinarySerializable binaryObject) {
            MemoryStream ms = new MemoryStream();
            BinaryWriter br = new BinaryWriter(ms);
            binaryObject.Serialize(br);
            byte[] bytes = ms.ToArray();
            ms.Close();
            br.Close();
            return bytes;
        }
    }
}
