using System;
using MediatR;

namespace MediatorPattern.Notifications
{
    public class PessoaDeleteNotification : INotification
    {
        public int Id { get; set; }
        public bool IsConcluido { get; set; }
    }
}