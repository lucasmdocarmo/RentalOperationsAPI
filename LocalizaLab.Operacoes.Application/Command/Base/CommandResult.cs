using LocalizaLab.Operacoes.Domain.Command;
using System;
using System.Collections.Generic;
using System.Text;
using Flunt.Notifications;

namespace LocalizaLab.Operacoes.Application.Command
{
    public class CommandResult : ICommandResult
    {
        public CommandResult() { }
        public CommandResult(bool success) { Success = success; }
        public CommandResult(bool success, IReadOnlyCollection<Notification> messages)
        {
            Success = success;
            Messages = messages;
        }
        public CommandResult(bool success, object result)
        {
            Result = result;
            Success = success;
        }

        public bool Success { get; set; }
        public object Result { get; set; }
        public IReadOnlyCollection<Notification> Messages { get; set; }
    }
}
