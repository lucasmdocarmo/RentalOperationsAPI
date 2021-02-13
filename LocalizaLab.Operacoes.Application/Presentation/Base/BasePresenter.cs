using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocalizaLab.Operacoes.Application.Presentation
{
    public class BasePresenter : Notifiable, IBusinessErrorPort
    {
        protected ActionResult ApplicationResult { get; set; }
        public ActionResult Model => ApplicationResult;

        public void BusinessError(string errorMessage)
        {
            ApplicationResult = new UnprocessableEntityObjectResult(errorMessage);
        }
    }
    public interface IBusinessErrorPort
    {
        void BusinessError(string errorMessage);
    }
}
