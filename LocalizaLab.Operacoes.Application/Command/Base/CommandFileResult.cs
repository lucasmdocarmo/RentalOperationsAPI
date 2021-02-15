using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Command.Base
{
    public class CommandFileResult :CommandResult
    {
        public byte[] ResultFile { get; set; }
        public CommandFileResult(byte[] arquivo)
        {
            ResultFile = arquivo;
        }
    }
}
