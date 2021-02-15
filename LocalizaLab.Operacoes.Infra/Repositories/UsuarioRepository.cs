using LocalizaLab.Operacoes.Domain.Entities.Usuarios;
using LocalizaLab.Operacoes.Domain.Shared.Repository;
using LocalizaLab.Operacoes.Infra.Context;
using LocalizaLab.Operacoes.Infra.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace LocalizaLab.Operacoes.Infra.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(OperacoesContext db) : base(db) { }

        public async Task<Usuario> GetUsuarioByLogin(string login, string senha)
        {
            var result = await base.Search(user => user.Login == login && user.Senha == senha).ConfigureAwait(true);
            if(result is null)
            {
                return null;
            }
            return result.FirstOrDefault();
        }
        public async Task CadastrarOperador(Usuario user)
        {
            await base.Add(user).ConfigureAwait(true);
        }
        public async Task CadastrarUsuario(Usuario user)
        {
            await base.Add(user).ConfigureAwait(true);
        }
    }
}
