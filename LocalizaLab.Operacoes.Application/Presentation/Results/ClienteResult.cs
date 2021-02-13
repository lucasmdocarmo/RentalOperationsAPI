using LocalizaLab.Operacoes.Domain.ValueObjects.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Queries.Results
{
    public class ClienteResult
    {
        public ClienteResult(string nome, string identidade, string email, DateTime dataNascimento, CPF cPF)
        {
            Nome = nome;
            Identidade = identidade;
            Email = email;
            DataNascimento = dataNascimento;
            CPF = cPF;
        }

        public string Nome { get; set; }
        public string Identidade { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public CPF CPF { get; set; }
    }
}
