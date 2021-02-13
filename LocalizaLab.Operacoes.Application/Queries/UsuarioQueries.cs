using AutoMapper;
using AutoMapper.QueryableExtensions;
using Flunt.Notifications;
using LocalizaLab.Operacoes.Application.Queries.Base;
using LocalizaLab.Operacoes.Application.Queries.Models;
using LocalizaLab.Operacoes.Application.Queries.Results;
using LocalizaLab.Operacoes.Domain.Entities.Clientes;
using LocalizaLab.Operacoes.Domain.Entities.Usuarios;
using LocalizaLab.Operacoes.Domain.Extensions;
using LocalizaLab.Operacoes.Domain.Queries;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Application.Queries
{
    public class UsuarioQueries: Notifiable, IQueryHandler<UsuarioQuery>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IOperadorRepository _operadorRepository;
        private readonly IMapper _mapper;
        public UsuarioQueries(IUsuarioRepository usuario, IClienteRepository clienteRepository, IOperadorRepository operadorRepository, IMapper mapper)
        {
            _usuarioRepository = usuario;
            _clienteRepository = clienteRepository;
            _operadorRepository = operadorRepository;
            _mapper = mapper;
        }

        public async ValueTask<IQueryResult> Handle(UsuarioQuery command)
        {
            var queryResult = new QueryResult();

            var usuario = await _usuarioRepository.GetUsuarioByLogin(command.Login);

            if(usuario != null)
            {
                var cliente = await _clienteRepository.GetClienteByCPF(usuario.Login).ConfigureAwait(true);

                var resultado = cliente.ProjectTo<ClienteResult>(_mapper.ConfigurationProvider);

                return new QueryResult(true, resultado);
            }
            else 
            {
                var operador = await _operadorRepository.GetOperadorByCPF(command.Login);
                queryResult.Result = operador;

                return new QueryResult(true, operador);
            }
           
        }
    }
}
