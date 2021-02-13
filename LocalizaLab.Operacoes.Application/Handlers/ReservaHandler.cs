using Flunt.Notifications;
using LocalizaLab.Operacoes.Application.Command.Reservas;
using LocalizaLab.Operacoes.Domain.Command;
using LocalizaLab.Operacoes.Domain.Command.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Application.Handlers
{
    public class ReservaHandler : Notifiable, ICommandHandler<SimularReservaCommand>
    {
        public ValueTask<ICommandResult> Handle(SimularReservaCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
