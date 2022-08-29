using System;
using MediatR;

namespace MediatorPattern.Notifications
{
    public class PessoaUpdateNotification : INotification
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public bool IsConcluido { get; set; }
    }
}