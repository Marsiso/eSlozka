using MediatR;

namespace eSlozka.Domain.Services;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
