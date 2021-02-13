using Flunt.Notifications;
using LocalizaLab.Operacoes.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocalizaLab.Operacoes.Application.Queries.Base
{
    public class QueryResult : IQueryResult
    {
        public QueryResult() { }
        public QueryResult(bool success, object result)
        {
            Success = success;
            Result = result;
        }

        public bool Success { get; set; }
        public object Result { get; set; }
    }
}
