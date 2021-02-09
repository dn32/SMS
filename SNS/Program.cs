using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;

namespace SNS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var serial = new SerialPort("COM8", 2400, Parity.None, 8, StopBits.One);
            Console.WriteLine($"Abrindo a porta");
            serial.Open();
            Console.WriteLine($"Porta aberta");
            serial.DataReceived += Rx;

            while (true)
            {
                var bytes = new byte[] { 0x51, 0xFF, 0xFF, 0xFF, 0xFF, 0xB3, 0x0D };
                serial.Write(bytes, 0, bytes.Length);
                await Task.Delay(3000);
            }

        }

        public static List<int> Bytes { get; set; } = new List<int>();

        private static void Rx(object sender, SerialDataReceivedEventArgs e)
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
                    Console.WriteLine(p);
            }
        }
    }

    public class Package
    {
        public static Package Create(List<int> bytes)
        {
            var tipo = bytes.Next(1);
            if (!new int[] { 60, 61, 62 }.Contains(tipo))
            {
                bytes.Clear();
                return null;
            }

            return new Package(bytes);
        }

        public override string ToString()
        {
            return $"In:{TensaoEntrada}V, Out: {TensaoSaida}V, Pwr: {PotenciaSaida}%, Temp: {Temperatura}º, Bat: {PorcentagemTensaoBateria}% ({(Status.ByPass ? "AC" : "Bateria")})";
        }

        private Package(List<int> bytes)
        {
            UltimaTensao = bytes.Next(2) / 10;
            TensaoEntrada = bytes.Next(2) / 10;
            TensaoSaida = bytes.Next(2) / 10;
            PotenciaSaida = bytes.Next(2) / 10;
            FrequenciaSaida = bytes.Next(2) / 10;
            PorcentagemTensaoBateria = bytes.Next(2) / 10;
            Temperatura = bytes.Next(2) / 10;
            EstadoBateria = bytes[0] + bytes[1] * 256;

            var bits = new BitArray(BitConverter.GetBytes(EstadoBateria).ToArray());
            Status = new Status
            {
                BeepLigado = bits[0],
                ShutdownAtivo = bits[1],
                TesteAtivo = bits[2],
                UpsOk = bits[3],
                Boost = bits[4],
                ByPass = bits[5],
                BateriaBaixa = bits[6],
                BateriaLigada = bits[7]
            };

            //Console.WriteLine(@$"BeepLigado: {status.BeepLigado},ShutdownAtivo: {status.ShutdownAtivo},TesteAtivo: {status.TesteAtivo},UpsOk: {status.UpsOk},Boost: {status.Boost},ByPass: {status.ByPass},BateriaBaixa: {status.BateriaBaixa},BateriaLigada: {status.BateriaLigada}");
        }

        public int UltimaTensao { get; set; }
        public int TensaoEntrada { get; set; }
        public int TensaoSaida { get; set; }
        public int PotenciaSaida { get; set; }
        public int FrequenciaSaida { get; set; }
        public int PorcentagemTensaoBateria { get; set; }
        public int Temperatura { get; set; }
        public int EstadoBateria { get; set; }
        public Status Status { get; set; }

    }
    public class Status
    {
        public bool BeepLigado;
        public bool ShutdownAtivo;
        public bool TesteAtivo;
        public bool UpsOk;
        public bool Boost;
        public bool ByPass;
        public bool BateriaBaixa;
        public bool BateriaLigada;
    }

    public static class Util
    {
        public static bool Bit(this string value, int index)
        {
            return value.Length >= index + 1 && value[index] == '1';
        }

        public static int Next(this List<int> list, int count)
        {
            int done = 1;
            var element = 0;

        run:
            var b = list[0];
            element += b;
            list.RemoveAt(0);
            if (count > done)
            {
                element *= 256;
                done++;
                goto run;
            }

            return element;
        }
    }

}
