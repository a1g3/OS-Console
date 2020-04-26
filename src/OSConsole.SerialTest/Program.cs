using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace OSConsole.SerialTest
{
    public class Program
    {
        private static SerialPort _SerialPort { get; set; }
        public static int bufSize = 1024;
        public static void Main(string[] args)
        {
            var portNames = SerialPort.GetPortNames();
            Console.WriteLine("The following ports are avaliable:");
            foreach (var p in portNames)
                Console.WriteLine($"{p}");

            Console.WriteLine("Enter a COM port!");
            string portName = Console.ReadLine();
            Console.WriteLine($"Connecting to {portName}");
            _SerialPort = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);
            _SerialPort.Handshake = Handshake.None;
            _SerialPort.DataReceived += port_OnReceiveData;
            _SerialPort.Open();
            Console.WriteLine("Write to console:");

            ConsoleKeyInfo input = Console.ReadKey(true);
            while (input.Key != ConsoleKey.Delete)
            {
                if (input.Key == ConsoleKey.Enter)
                    _SerialPort.Write("\n\r");
                else
                    _SerialPort.Write(input.KeyChar.ToString());
                input = Console.ReadKey(true);
            }
            _SerialPort.Close();
            Console.Read();
        }

        private static void port_OnReceiveData(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = (SerialPort)sender;
            switch (e.EventType)
            {
                case SerialData.Chars:
                    {
                        var buf = new byte[bufSize];
                        port.Read(buf, 0, bufSize);
                        var printBuf = buf.Where(x => x != 0).ToArray();
                        for (int i = 0; i < printBuf.Length; i++)
                        {
                            if (printBuf[i] == 10 && i > printBuf.Length && printBuf[i + 1] == 13)
                                Console.Write(Environment.NewLine);
                            else
                                Console.Write((char)printBuf[i]);
                        }
                        break;
                    }
                case SerialData.Eof:
                    {
                        // means receiving ended
                        break;
                    }
            }
        }
        public static void Read()
        {
            while (_SerialPort.IsOpen)
            {
                try
                {
                    string message = _SerialPort.ReadLine();
                    Console.WriteLine(message);
                }
                catch (TimeoutException) { }
            }
        }
    }
}
 