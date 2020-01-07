using System.IO;

namespace Ogx {

    public interface IBinarySerializable {

        void Serialize(BinaryWriter writer);
        void Deserialize(BinaryReader2 reader);
    }
}
