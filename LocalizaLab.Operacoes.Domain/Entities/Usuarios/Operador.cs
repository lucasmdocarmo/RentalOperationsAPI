using LocalizaLab.Operacoes.Domain.Shared.Entities;
using LocalizaLab.Operacoes.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Entities.Usuarios
{
    public sealed class Operador : BaseEntity
    {
        public Operador(string matricula, string nome, string senha)
        {
            Matricula = matricula;
            Nome = nome;
            Senha = senha;
            Perfil = ETipoPerfil.Operador;
        }

        public string Matricula { get; private set; }
        public string Nome { get; private set; }
        public string Senha { get; private set; }
        public ETipoPerfil Perfil { get; private set; }
    }
}
