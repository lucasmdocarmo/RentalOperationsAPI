using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Command
{
    public interface ICommand { bool Validate(); }
    public interface ICommandResult { }
}
