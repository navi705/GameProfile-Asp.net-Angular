using MediatR;

namespace GameProfile.Application.CQRS;

public interface ICommand : IRequest
{
}

// public interface ICommand<TResponse> : IRequest<Result<TResponse>>
