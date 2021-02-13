using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Domain.Exceptions
{
    public sealed class DomainException : Exception
    {
        public override string Message { get; }
        public DomainException(string message) : base(message)
        {
            this.Message = message;
        }
    }
}
