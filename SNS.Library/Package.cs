using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SNS.Library
{
    public class Package
    {
        public int UltimaTensao { get; set; }
        public int TensaoEntrada { get; set; }
        public int TensaoSaida { get; set; }
        public int PotenciaSaida { get; set; }
        public int FrequenciaSaida { get; set; }
        public int PorcentagemTensaoBateria { get; set; }
        public int Temperatura { get; set; }
        private int EstadoBateria { get; set; }
        public Status Status { get; set; }
        public DateTime Date { get; }

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
           return $"{Date:HH:mm ss} - In: {TensaoEntrada}V - Out: {TensaoSaida}V - Pwr: {PotenciaSaida}% - Temp: {Temperatura}º - Bat: {PorcentagemTensaoBateria}% ({(Status.ByPass ? "AC" : "Bateria")})";
        }

        private Package(List<int> bytes)
        {
            Date = DateTime.Now;
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
        }
    }
}
