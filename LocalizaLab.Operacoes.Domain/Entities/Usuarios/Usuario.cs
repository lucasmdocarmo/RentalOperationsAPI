using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using LocalizaLab.Operacoes.Domain.Exceptions;
using LocalizaLab.Operacoes.Domain.Shared.Entities;
using LocalizaLab.Operacoes.Domain.ValueObjects.Clientes;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LocalizaLab.Operacoes.Domain.Entities.Usuarios
{
    public sealed class Usuario : BaseEntity
    {
        public Usuario() { }

        public Usuario(string nome, string login, string senha)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            Perfil = ETipoPerfil.Cliente;
        }

        public string Nome { get; set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public ETipoPerfil Perfil { get; private set; }
        public CPF CPF { get; private set; }

        public Guid ClienteId { get; private set; }
        public Cliente Cliente { get; private set; }

        public void VincularClienteAoUsuario(Cliente cliente)
        {
            ClienteId = cliente.Id;
            CPF = cliente.CPF;
        }
    }
}
