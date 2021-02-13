using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.API.Configuration
{
    public class APIProfile
    {
        public static string Name => "LL.Operations.Rental";
        public static string Description => "Localiza Labs. API para cadastro de Veiculos, Clientes, Operador e Geração de Contratos e Reservas.";
        public static IDictionary<string, string> VersioningDescriptions => new Dictionary<string, string>();
        public static string DefaultCultureInfo => "pt-BR";
    }
}