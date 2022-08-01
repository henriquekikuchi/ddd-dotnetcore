using MediatR;

namespace VemDeZap.Domain.Commands.Usuario.AutenticarUsuario;

public class AutenticarUsuarioRequest : IRequest<AutenticarUsuarioResponse>
{
    public string Email { get; set; }
    public string Senha { get; set; }
}
