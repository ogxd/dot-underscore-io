using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ogx {

    public class BinaryPropertyList : IBinarySerializable {

        public string version;
        public BinaryProperty property;

        public BinaryPropertyList() {
            
        }

        public BinaryPropertyList(BinaryReader2 reader) {
            Deserialize(reader);
        }

        public override string ToString() {
            return "bplist" + version;
        }

        public void Deserialize(BinaryReader2 reader) {

            version = Encoding.UTF8.GetString(reader.ReadBytes(2));
            switch (version) {
                case "00":
                    property = BinaryProperty.ReadBinaryProperty(reader);
                    break;
                default:
                    Console.WriteLine($"Unsupported binary property list version : {version}");
                    break;
            }
        }

        public void Serialize(BinaryWriter writer) {
            
        }
    }
}
