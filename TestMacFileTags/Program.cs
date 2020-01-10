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
            ReadFile(files[0]);

            Console.ReadKey();
        }

        static void ReadFile(string file)
        {
            Console.Write("Reading : " + file);
            DotUnderscore dotUnderscore = BinaryHelper.Read<DotUnderscore>(file);
            Attribute tagAttribute = dotUnderscore.attributes.attributes.Where(x => x.Name == "com.apple.metadata:_kMDItemUserTags\0").FirstOrDefault();

            if (tagAttribute == null) {
                Console.WriteLine("There are no tags attribute !");
                return;
            }

            var bplist = tagAttribute.Value as BinaryPropertyList;
            var tagsArray = bplist.property as BinaryArray;
            if (tagsArray == null) {
                return;
            }

            foreach (BinaryStringASCII binaryString in tagsArray.properties) {
                var values = binaryString.value.Split('\n');
                string tagName = values[0];
                int tagColor = int.Parse(values[1]);
                Console.WriteLine($"{tagName}:{(TagColor)tagColor}");
            }
        }
    }
}
