using System;
using MediatorPattern.Domain.Command;
using MediatorPattern.Domain.Entity;
using MediatorPattern.Notifications;
using MediatorPattern.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace MediatorPattern.Domain.Handler
{
    public class PessoaCreateCommandHandler : IRequestHandler<PessoaCreateCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Pessoa> _repository;

        public PessoaCreateCommandHandler(IMediator mediator, IRepository<Pessoa> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }

        public async Task<string> Handle(PessoaCreateCommand request, CancellationToken cancellationToken)
        {
            Int64 id = _repository.MaiorId() + 1;

            var produto = new Pessoa { Id = (int)id, Nome = request.Nome, Idade = request.Idade };

            try
            {
                await _repository.Add(produto);
                await _mediator.Publish(new PessoaCreateNotification { Id = produto.Id, Nome = produto.Nome, Idade = produto.Idade });

                return await Task.FromResult("Pessoa criada com sucesso");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new PessoaCreateNotification { Id = produto.Id, Nome = produto.Nome, Idade = produto.Idade });
                await _mediator.Publish(new ErroNotification { Erro = ex.Message, PilhaErro = ex.StackTrace });

                return await Task.FromResult("Ocorreu um erro no momento da criação");
            }
        }
    }
}