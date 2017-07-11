using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Playground.MediatrPipelines
{
    // TODO Autenticação.
    // TODO Inserir, alterar, deletar com validação.
    [Route("values")]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;
        private IMediator _mediator;

        public ValuesController(ILogger<ValuesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetValues(ValuesRequest valuesQuery)
        {
            var queryResult = await _mediator.Send(valuesQuery);
            return Ok(queryResult);
        }
    }
}
