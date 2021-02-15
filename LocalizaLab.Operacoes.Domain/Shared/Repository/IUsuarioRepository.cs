using LocalizaLab.Operacoes.Domain.Contracts.Repository.Base;
using LocalizaLab.Operacoes.Domain.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Domain.Shared.Repository
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> GetUsuarioByLogin(string login, string senha);
        Task CadastrarOperador(Usuario user);
        Task CadastrarUsuario(Usuario user);
    }
}
