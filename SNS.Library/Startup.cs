using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;

namespace SNS.Library
{
    public class Startup
    {
        public List<int> Bytes { get; set; } = new List<int>();

        public Action<Package> Callback { get; set; }

        public bool Run(Action<Package> callback, TimeSpan frequency)
        {
            try
            {
                Callback = callback;
                var port = SerialPort.GetPortNames().FirstOrDefault();
                if (string.IsNullOrWhiteSpace(port)) return false;
                var serial = new SerialPort(port, 2400, Parity.None, 8, StopBits.One);
                Console.WriteLine($"Abrindo a porta");
                serial.Open();
                Console.WriteLine($"Porta aberta");
                serial.DataReceived += Rx;

                _ = Task.Run(async () =>
                {
                    while (true)
                    {
                        var bytes = new byte[] { 0x51, 0xFF, 0xFF, 0xFF, 0xFF, 0xB3, 0x0D };
                        serial.Write(bytes, 0, bytes.Length);
                        await Task.Delay(frequency);
                    }
                });

                return true;
            }
            catch (Exception)
            {
                return false;
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
