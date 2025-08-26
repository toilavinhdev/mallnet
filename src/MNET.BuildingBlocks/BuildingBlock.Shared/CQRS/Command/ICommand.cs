using MediatR;

namespace BuildingBlock.Shared.CQRS.Command;

public interface ICommand : IRequest;

public interface ICommand<out TResponse> : IRequest<TResponse>;