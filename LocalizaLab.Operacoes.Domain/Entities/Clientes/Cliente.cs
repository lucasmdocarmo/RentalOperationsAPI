using LocalizaLab.Operacoes.Domain.Shared.Entities;
using LocalizaLab.Operacoes.Domain.ValueObjects.Clientes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LocalizaLab.Operacoes.Domain.Entities.Contratos;
using LocalizaLab.Operacoes.Domain.Entities.Usuarios;

namespace LocalizaLab.Operacoes.Domain.Entities.Clientes
{
    public class Cliente : BaseEntity
    {
        internal Cliente() { }
        public Cliente(string nome, string identidade, string email, DateTime dataNascimento, CPF cPF)
        {
            Nome = nome;
            Identidade = identidade;
            Email = email;
            DataNascimento = dataNascimento;
            CPF = cPF;
        }

        public string Nome { get; private set; }
        public string Identidade { get; private set; }
        public string Email { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public CPF CPF { get; private set; }


        // EF
        public IReadOnlyCollection<Contrato> Contratos { get; set; }
        public IReadOnlyCollection<Reserva> Reservas { get; set; }
        public Endereco Endereco { get; set; }
        public Usuario Usuario { get; set; }
    }
}