using MediatR;

namespace eSlozka.Domain.Services;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}