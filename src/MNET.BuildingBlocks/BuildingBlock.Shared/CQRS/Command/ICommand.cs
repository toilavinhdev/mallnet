using MediatR;

namespace BuildingBlock.Shared.CQRS.Command;

public interface ICommand : ICommand<Unit>;

public interface ICommand<out TResponse> : IRequest<TResponse>;