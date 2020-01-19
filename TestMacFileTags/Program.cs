using System;
using System.IO;
using System.Linq;

namespace Ogx
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.GetFiles(@"C:\TestFiles\");
            //ReadFile(files[0]);
            //ReadFile(files[1]);

            WriteFile();

            Console.ReadKey();
        }

        static void ReadFile(string file)
        {
            Console.WriteLine(File.ReadAllBytes(file).Length);

            Console.WriteLine("Reading : " + file);
            DotUnderscore dotUnderscore = BinaryHelper.Read<DotUnderscore>(file);
            Attribute tagAttribute = (dotUnderscore.entries[0].data as AttributesHeader).attributes.Where(x => x.name == "com.apple.metadata:_kMDItemUserTags\0").FirstOrDefault();

            if (tagAttribute == null) {
                Console.WriteLine("There are no tags attribute !");
                return;
            }

            var bplist = tagAttribute.value as BinaryPropertyList;
            var tagsArray = bplist.property as BinaryArray;
            if (tagsArray == null) {
                return;
            }

            foreach (BinaryStringASCII binaryString in tagsArray.properties) {
                var values = binaryString.value.Split('\n');
                string tagName = values[0];
                int tagColor = (values.Length > 1) ? int.Parse(values[1]) : 0;
                Console.WriteLine($"{tagName}:{(TagColor)tagColor}");
            }
        }

        static void WriteFile() {

            var dotUnderscore = new DotUnderscore();
            var entry = new Entry();
            var footerEntry = new FooterEntry();
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

            var bytes = BinaryHelper.Write(dotUnderscore);

            Console.WriteLine(bytes.Length);

            string path = @"C:\TestFiles\._Test.txt";
            File.WriteAllBytes(path, bytes);

            ReadFile(path);
        }
    }
}
