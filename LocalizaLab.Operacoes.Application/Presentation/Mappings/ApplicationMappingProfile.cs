using AutoMapper;
using LocalizaLab.Operacoes.Application.Queries.Results;
using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Presentation
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Cliente, ClienteResult>()
               .ConstructUsing(c => new ClienteResult(c.Nome,c.Identidade,c.Email,c.DataNascimento,c.CPF));
        }
    }
}
