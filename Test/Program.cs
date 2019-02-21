using Ponytail.Device.CodeScanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeScanner scanner = new CodeScanner("COM3", 21);
            scanner.DataReceived += Scanner_DataReceived;
            scanner.Open();
            Console.ReadLine();
            scanner.Close();
        }

        private static void Scanner_DataReceived(string code)
        {
            Console.WriteLine(code);
        }
    }
}
