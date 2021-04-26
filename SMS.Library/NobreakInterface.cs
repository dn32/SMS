using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace SMS.Library
{
    public class NobreakInterface
    {
        private List<int> Bytes { get; set; } = new List<int>();

        private Action<Package> Callback { get; set; }

        private SerialPort SerialPort { get; set; }

        private byte[] GetStatusData { get; } = new byte[] { 0x51, 0xFF, 0xFF, 0xFF, 0xFF, 0xB3, 0x0D };

        private byte[] SetBeepOnOfData { get; } = new byte[] { 0x4D, 0xFF, 0xFF, 0xFF, 0xFF, 0xB7, 0x0D };

        private byte[] SetTestOnData { get; } = new byte[] { 0x4C, 0xFF, 0xFF, 0xFF, 0xFF, 0xB8, 0x0D };

        private byte[] SetTestOffData { get; } = new byte[] { 0x44, 0xFF, 0xFF, 0xFF, 0xFF, 0xC0, 0x0D };

        public NobreakInterface(Action<Package> callback)
        {
            Callback = callback;
        }

        public (string port, int count) Connect(int tentativaDeConexao)
        {
            var ports = SerialPort.GetPortNames().OrderBy(x => x).ToList();// Todo - testar as outras portas

            if (SerialPort?.IsOpen == true)
                SerialPort.Close();
            
            Bytes.Clear();

            if (ports.Count <= tentativaDeConexao)
                tentativaDeConexao = 0;

            var port = ports[tentativaDeConexao];
            
            if (string.IsNullOrWhiteSpace(port)) throw new InvalidOperationException("Nobreak não está conectado");
            SerialPort = new SerialPort(port, 2400, Parity.None, 8, StopBits.One);
            SerialPort.Open();
            SerialPort.DataReceived += Rx;
            return (port, tentativaDeConexao);
        }

        public void GetStatus()
        {
            try
            {
                var bytes = GetStatusData;
                lock (SerialPort)
                    SerialPort.Write(bytes, 0, bytes.Length);
            }
            catch
            {
                try
                {
                    lock (SerialPort)
                    {
                        SerialPort.Close();
                    }
                    SerialPort.Dispose();
                }
                catch { }
                throw;
            }
        }

        public void SetTestOn()
        {
            var bytes = SetTestOnData;
            lock (SerialPort)
                SerialPort.Write(bytes, 0, bytes.Length);
        }

        public void SetTestOff()
        {
            var bytes = SetTestOffData;
            lock (SerialPort)
                SerialPort.Write(bytes, 0, bytes.Length);
        }

        public void SetBeepOnOf()
        {
            var bytes = SetBeepOnOfData;
            lock (SerialPort)
                SerialPort.Write(bytes, 0, bytes.Length);
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
