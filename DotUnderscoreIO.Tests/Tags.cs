using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogx;
using System.IO;

namespace DotUnderscoreIO.Tests
{
    [TestClass]
    public class Tags
    {
        [TestMethod]
        public void Serialization_Deserialization()
        {
            var dotUnderscore = new DotUnderscore();
            var entry = new Entry(9u);
            var footerEntry = new Entry(2u); // Footer
            footerEntry.id = 2;
            footerEntry.size = 286;
            footerEntry.offset = 3810;
            dotUnderscore.entries = new Entry[] { entry, footerEntry };
            var attrHeader = new AttributesHeader();
            entry.data = attrHeader;
            var tagAttribute = new Attribute();
            tagAttribute.name = "com.apple.metadata:_kMDItemUserTags\0";
            attrHeader.attributes.Add(tagAttribute);
            var bplist = new BinaryPropertyList();
            tagAttribute.value = bplist;
            var barray = new BinaryArray();
            bplist.property = barray;
            barray.properties = new BinaryProperty[2];
            barray.properties[0] = new BinaryStringASCII { value = "Rouge\n6" };
            barray.properties[1] = new BinaryStringASCII { value = "Orange\n7" };

            var bytes1 = BinaryHelper.Write(dotUnderscore);

            DotUnderscore dotUnderscore2 = BinaryHelper.Read<DotUnderscore>(bytes1);

            var bytes2 = BinaryHelper.Write(dotUnderscore2);

            Assert.AreEqual(bytes1.Length, bytes2.Length);
        }
    }
}
