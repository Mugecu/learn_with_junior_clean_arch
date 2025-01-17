using egisz_receive_residue.Application.UseCases.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace egisz_receive_residue.Controllers
{
    /// <summary>
    /// Контроллер для обработки операций с информацией об остатках.
    /// </summary>
    /// <remarks>
    /// Конструктор контроллера.
    /// </remarks>
    /// <param name="mediator">Медиатор для отправки команд.</param>
    [Route("api/[controller]")]
    [ApiController]
    public class TransferResiduesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Передача информации об остатках из БД сервиса остатков в БД рег. ФРЛЛО.
        /// </summary>
        /// <returns>OK (200) при успешном выполнении или BadRequest (400) при ошибке.</returns>
        [HttpPost]
        public async Task<ActionResult> TransferResiduesAsync()
        {
            await _mediator.Send(new TransferResiduesCommand());
            return Ok();
        }
    }
}