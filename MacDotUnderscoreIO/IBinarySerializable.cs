using System.IO;

namespace Ogx {

    public interface IBinarySerializable {

        void Serialize(BinaryWriter writer);
        void Deserialize(BinaryParser reader);
    }
}
