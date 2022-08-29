using System;
using MediatorPattern.Domain.Entity;
using MediatorPattern.Notifications;
using MediatorPattern.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using MediatorPattern.Domain.Command;

namespace MediatorPattern.Domain.Handler
{
    public class PessoaDeleteCommandHandler : IRequestHandler<PessoaDeleteCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Pessoa> _repository;

        public PessoaDeleteCommandHandler(IMediator mediator, IRepository<Pessoa> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }

        public async Task<string> Handle(PessoaDeleteCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                await _repository.Delete(request.Id);
                await _mediator.Publish(new PessoaDeleteCommand
                {
                    Id = request.Id,
                });

                return await Task.FromResult("Pessoa excluida com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new PessoaDeleteNotification
                {
                    Id = request.Id,
                    IsConcluido = false
                });

                await _mediator.Publish(new ErroNotification
                {
                    Erro = ex.Message,
                    PilhaErro = ex.StackTrace
                });

                return await Task.FromResult("Ocorreu um erro no momento da exclusão");
            }
        }
    }
}