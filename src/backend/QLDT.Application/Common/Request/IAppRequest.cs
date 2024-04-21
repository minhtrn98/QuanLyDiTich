namespace QLDT.Application.Common.Request;

public interface IQuery<TResponse> : IAuthorizeableRequest<Result<TResponse>> { }
public interface ICommand<TResponse> : IAuthorizeableRequest<Result<TResponse>> { }
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse> { }
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse> { }
