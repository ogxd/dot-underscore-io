using System;

namespace Ogx
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(BitConverter.IsLittleEndian);

            string file = @"C:\Users\ogini\Projects\EXT_Claunia.IO\._plan maison.png";

            DotUnderscore dotUnderscore = BinaryHelper.Read<DotUnderscore>(file);

            Console.Write(dotUnderscore);
            Console.ReadKey();
        }
    }
}
