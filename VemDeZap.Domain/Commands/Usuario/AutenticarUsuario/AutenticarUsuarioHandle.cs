

using MediatR;
using prmToolkit.NotificationPattern;
using VemDeZap.Domain.Extensions;
using VemDeZap.Domain.Interfaces.Repositories;

namespace VemDeZap.Domain.Commands.Usuario.AutenticarUsuario;

public class AutenticarUsuarioHandle : Notifiable, IRequestHandler<AutenticarUsuarioRequest, AutenticarUsuarioResponse>
{
    private readonly IMediator _mediator;
    private readonly IRepositoryUsuario _repositoryUsuario;

    public AutenticarUsuarioHandle(IMediator mediator, IRepositoryUsuario repositoryUsuario)
    {
        _mediator = mediator;
        _repositoryUsuario = repositoryUsuario;
    }

    public async Task<AutenticarUsuarioResponse> Handle(AutenticarUsuarioRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            AddNotification("Request", "Request é obrigatório");
            return null;
        }

        request.Senha = request.Senha.ConvertToMD5();

        Entities.Usuario usuario = _repositoryUsuario.ObterPor(x => x.Email == request.Email && x.Senha == request.Senha);

        if (usuario == null)
        {
            AddNotification("Usuario", "Usuário não encontrado.");
            return new AutenticarUsuarioResponse()
            {
                Autenticado = false
            };
        }

        var response = (AutenticarUsuarioResponse)usuario;

        return await Task.FromResult(response);
    }
}
