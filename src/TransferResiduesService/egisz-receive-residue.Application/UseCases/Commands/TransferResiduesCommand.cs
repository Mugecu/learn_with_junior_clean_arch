using MediatR;

namespace egisz_receive_residue.Application.UseCases.Commands
{
    /// <summary>
    /// Команда для передачи информации об остатках из БД сервиса остатков в БД рег. ФРЛЛО.
    /// </summary>
    public class TransferResiduesCommand : IRequest<Unit> { }
}