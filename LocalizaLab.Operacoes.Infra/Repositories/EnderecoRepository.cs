using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using LocalizaLab.Operacoes.Infra.Context;
using LocalizaLab.Operacoes.Infra.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Infra.Repositories
{
    public class EnderecoRepository : BaseRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(OperacoesContext db) : base(db) { }
    }
}
