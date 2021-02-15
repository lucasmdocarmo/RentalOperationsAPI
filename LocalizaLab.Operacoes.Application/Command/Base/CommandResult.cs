using LocalizaLab.Operacoes.Domain.Command;
using System;
using System.Collections.Generic;
using System.Text;
using Flunt.Notifications;
using System.Net;
using LocalizaLab.Operacoes.Application.Presentation;
using Microsoft.AspNetCore.Mvc;

namespace LocalizaLab.Operacoes.Application.Command
{
    public class CommandResult : ICommandResult
    {
        public byte[] ResultFile { get; set; }
        public ActionResult ViewModel { get; private set; }
        public CommandResult() { }
        public CommandResult(bool success) 
        { 
            Success = success; 
        }
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
        public CommandResult(HttpStatusCode status, object result)
        {
            Status = status;
            Result = result;
        }
        public CommandResult(byte[] arquivo)
        {
            ResultFile = arquivo;
        }

        private ActionResult ResultAdapter()
        {
            switch (this.Status)
            {
                case HttpStatusCode.OK:
                    ViewModel = new OkObjectResult("");
                    break;

            }

            return ViewModel;
        }

        public HttpStatusCode Status { get; set; }
        public bool Success { get; set; }
        public object Result { get; set; }
        public IReadOnlyCollection<Notification> Messages { get; set; }
    }
}
