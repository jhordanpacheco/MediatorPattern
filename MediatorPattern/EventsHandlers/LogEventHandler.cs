using System;
using System.Threading;
using System.Threading.Tasks;
using MediatorPattern.Notifications;
using MediatR;

namespace MediatorPattern
{
    public class LogEventHandler :
                        INotificationHandler<PessoaCreateNotification>,
                        INotificationHandler<PessoaUpdateNotification>,
                        INotificationHandler<PessoaDeleteNotification>,
                        INotificationHandler<ErroNotification>
    {
        public Task Handle(PessoaCreateNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"CRIACAO: '{notification.Id} " +
                    $"- {notification.Nome} - {notification.Idade}'");
            });
        }

        public Task Handle(PessoaUpdateNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ALTERACAO: '{notification.Id} - {notification.Nome} " +
                    $"- {notification.Idade} - {notification.IsConcluido}'");
            });
        }

        public Task Handle(PessoaDeleteNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"EXCLUSAO: '{notification.Id} " +
                    $"- {notification.IsConcluido}'");
            });
        }

        public Task Handle(ErroNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERRO: '{notification.Erro} \n {notification.PilhaErro}'");
            });
        }
    }
}