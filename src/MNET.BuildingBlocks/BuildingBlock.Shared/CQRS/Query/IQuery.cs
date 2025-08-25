using MediatR;

namespace BuildingBlock.Shared.CQRS.Query;

public interface IQuery<out TResponse> : IRequest<TResponse>;