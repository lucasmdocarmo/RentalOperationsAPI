using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Domain.Command.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        ValueTask<ICommandResult> Handle(T command);
    }
}