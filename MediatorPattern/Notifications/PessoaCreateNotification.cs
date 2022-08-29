using System;
using MediatR;

namespace MediatorPattern.Notifications
{
    public class PessoaCreateNotification : INotification
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}