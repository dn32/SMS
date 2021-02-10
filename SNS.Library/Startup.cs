using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;

namespace SNS.Library
{
    public class Startup
    {
        public List<int> Bytes { get; set; } = new List<int>();

        public Action<Package> Callback  { get; set; }

        public async Task Run(Action<Package> callback, TimeSpan frequency)
        {
            Callback = callback;

            var serial = new SerialPort("COM8", 2400, Parity.None, 8, StopBits.One);
            Console.WriteLine($"Abrindo a porta");
            serial.Open();
            Console.WriteLine($"Porta aberta");
            serial.DataReceived += Rx;

            while (true)
            {
                var bytes = new byte[] { 0x51, 0xFF, 0xFF, 0xFF, 0xFF, 0xB3, 0x0D };
                serial.Write(bytes, 0, bytes.Length);
                await Task.Delay(frequency);
            }
        }

        private void Rx(object sender, SerialDataReceivedEventArgs e)
        {
            var port = sender as SerialPort;
            var count = port.ReceivedBytesThreshold;
            for (int i = 0; i < count; i++)
            {
                var byte_ = port.ReadByte();
                Bytes.Add(byte_);
            }

            if (Bytes.Count >= 19)
            {
                var p = Package.Create(Bytes);
                if (p != null)
                {
                    Callback(p);
                }
                //Console.WriteLine(p);
            }
        }
    }
}
