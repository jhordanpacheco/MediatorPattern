using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatorPattern.Domain.Entity;
using MediatorPattern.Repository;
using MediatorPattern.Domain.Command;
using Microsoft.AspNetCore.Http;

namespace MediatorPattern.Controllers
{
    [Route("api/[controller]")]
    [Microsoft.AspNetCore.Mvc.ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IRepository<Pessoa> _repository;

        public PessoasController(IMediator mediator, IRepository<Pessoa> repository)
        {
            this._mediator = mediator;
            this._repository = repository;
        }

        //GET: api/pessoas/listar  
        /// <summary>
        /// Listar todas as pessoas
        /// </summary>
        [HttpGet]
        [Route("listar")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pessoa))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _repository.GetAll());
        }

        //GET: api/pessoas/obter/{id}  
        /// <summary>
        /// Obtem a pessoa via {id}
        /// </summary>
        [HttpGet]
        [Route("obter/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pessoa))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Obter(int id)
        {
            return Ok(await _repository.Get(id));
        }

        //POST: api/pessoas/incluir  
        /// <summary>
        /// Inclui uma nova pessoa
        /// </summary>
        [HttpPost]
        [Route("incluir")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Incluir([FromBody] PessoaCreateCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        //PUT: api/pessoas/alterar  
        /// <summary>
        /// Alterar uma pessoa
        /// </summary>
        [HttpPut]
        [Route("alterar")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Alterar([FromBody] PessoaUpdateCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        //DELETE: api/pessoas/deletar/{id}  
        /// <summary>
        /// Deletar uma pessoa a partir do {id}
        /// </summary>
        [HttpDelete]
        [Route("deletar/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deletar(int id)
        {
            var obj = new PessoaDeleteCommand { Id = id };
            var result = await _mediator.Send(obj);

            return Ok(result);
        }
    }
}