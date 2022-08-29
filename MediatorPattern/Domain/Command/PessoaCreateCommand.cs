using System;
using MediatR;

namespace MediatorPattern.Domain.Command
{
    public class PessoaCreateCommand : IRequest<string>
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}