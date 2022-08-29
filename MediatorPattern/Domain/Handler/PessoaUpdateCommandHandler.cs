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
    public class PessoaUpdateCommandHandler : IRequestHandler<PessoaUpdateCommand, string>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Pessoa> _repository;

        public PessoaUpdateCommandHandler(IMediator mediator, IRepository<Pessoa> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }

        public async Task<string> Handle(PessoaUpdateCommand request, CancellationToken cancellationToken)
        { 
            var pessoa = new Pessoa
            {
                Id = request.Id,
                Nome = request.Nome,
                Idade = request.Idade
            };

            try
            {
                await _repository.Edit(pessoa);
                await _mediator.Publish(new PessoaUpdateNotification
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    Idade = pessoa.Idade
                });

                return await Task.FromResult("Pessoa alterada com sucesso");
            }

            catch (Exception ex)
            {
                await _mediator.Publish(new PessoaUpdateNotification
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    Idade = pessoa.Idade
                });

                await _mediator.Publish(new ErroNotification
                {
                    Erro = ex.Message,
                    PilhaErro = ex.StackTrace
                });

                return await Task.FromResult("Ocorreu um erro no momento da alteração");
            }
        }
    }
}