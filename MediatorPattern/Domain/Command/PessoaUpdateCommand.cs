using System;
using MediatR;

namespace MediatorPattern.Domain.Command
{
    public class PessoaUpdateCommand : IRequest<string>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}