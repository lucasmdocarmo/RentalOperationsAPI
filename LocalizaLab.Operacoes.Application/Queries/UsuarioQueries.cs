using AutoMapper;
using AutoMapper.QueryableExtensions;
using Flunt.Notifications;
using LocalizaLab.Operacoes.Application.Presentation.Results.Operador;
using LocalizaLab.Operacoes.Application.Presentation.Results.Usuarios;
using LocalizaLab.Operacoes.Application.Queries.Base;
using LocalizaLab.Operacoes.Application.Queries.Models;
using LocalizaLab.Operacoes.Application.Queries.Results;
using LocalizaLab.Operacoes.Application.Queries.Usuarios;
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
    public class UsuarioQueries : Notifiable, IQueryHandler<UsuarioQuery>,IQueryHandler<ListarUsuarios>,IQueryHandler<ListarOperadores>
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
            var usuario = await _usuarioRepository.GetUsuarioByLogin(command.Login, command.Senha);
            if (usuario is null)
            { 
                AddNotification("Usuario/Operador", "Login e/ou Senha Incorreta.");
                return new QueryResult(false, base.Notifications);
            }

            var cliente = await _clienteRepository.GetClienteByCPF(usuario.Login).ConfigureAwait(true);
            if(cliente != null)
            {
                var resultado = cliente.ProjectTo<ClienteResult>(_mapper.ConfigurationProvider);
                return new QueryResult(true, resultado);
            }
           
            var operador = await _operadorRepository.GetOperadorByCPF(command.Login);
            if (operador == null)
            {
                AddNotification("Usuario/Operador", "Usuario e/ou Operador informados Nao Encontrado.");
                return new QueryResult(false, base.Notifications);
            }

            return new QueryResult(true, operador);
        }

        public async ValueTask<IQueryResult> Handle(ListarUsuarios command)
        {
            var usuarios = await _usuarioRepository.GetAll();
            var list = new ListarUsuariosResult();

            foreach (var item in usuarios)
            {
                list.Usuarios.Add(new UsuarioQueryList() 
                {
                    Login = item.Login,
                    Nome = item.Nome
                });
            }
            return new QueryResult(true, list);
        }

        public async ValueTask<IQueryResult> Handle(ListarOperadores command)
        {
            var operadores = await _operadorRepository.GetAll();
            var list = new ListarOperadoresResult();

            foreach (var item in operadores)
            {
                list.Operadores.Add(new OperadoresQueryList()
                {
                    Matricula = item.Matricula,
                    Nome = item.Nome
                });
            }

            return new QueryResult(true, list);
        }
    }
}
