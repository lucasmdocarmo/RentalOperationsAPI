using Flunt.Notifications;
using LocalizaLab.Operacoes.Application.Command.Contratos;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Application.Handlers
{
    public class ContratoHandler : Notifiable, ICommandHandler<CadastrarContratoCommand>
    {
        public ValueTask<ICommandResult> Handle(CadastrarContratoCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
