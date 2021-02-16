using LocalizaLab.Operacoes.Domain.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Presentation.Results.Usuarios
{
    public class ListarUsuariosResult
    {
       public ICollection<UsuarioQueryList> Usuarios { get; set; }
    }
    public class UsuarioQueryList
    {
        public string Nome { get; set; }
        public string Login { get; set; }
    }
}
