namespace RegistrationWizard.Server.Application;

public interface IHandler<TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken ct = default);
}
