using System;
using MediatR;

namespace MediatorPattern.Domain.Command
{
    public class PessoaDeleteCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}