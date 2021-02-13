using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Extensions
{
    internal static class OperacoesCodigosGenerator
    {
        private static readonly Random _random = new Random();
        public static string GenerateCodigoReserva(string agencia)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return "RES" + agencia + _random.Next(0, 999999) + _random.Next(0, chars.Length - 1);
        }
        public static string GenerateCodigoContrato(string agencia)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return "LOC" + agencia + _random.Next(0, 999999) + _random.Next(0, chars.Length - 1);
        }
    }
}
