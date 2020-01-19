namespace Ogx {

    public interface IBinarySerializable {

        void Serialize(BitWriter writer);
        void Deserialize(BitReader reader);
    }
}
