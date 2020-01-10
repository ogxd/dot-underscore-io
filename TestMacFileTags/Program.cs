using System;
using System.IO;

namespace Ogx
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.GetFiles(@"C:\TestFiles\");
            ReadFile(files[3]);

            Console.ReadKey();
        }

        static void ReadFile(string file)
        {
            Console.Write("Reading : " + file);
            DotUnderscore dotUnderscore = BinaryHelper.Read<DotUnderscore>(file);
            Console.Write(dotUnderscore);
        }
    }
}
