using MediatR;
using prmToolkit.NotificationPattern;
using VemDeZap.Domain.Interfaces.Repositories;

namespace VemDeZap.Domain.Commands.Usuario.AdicionarUsuario
{
    public class AdicionarUsuarioHandler : Notifiable, IRequestHandler<AdicionarUsuarioRequest, Response> 
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryUsuario _repositoryUsuario;

        public AdicionarUsuarioHandler(IMediator mediator, IRepositoryUsuario repositoryUsuario)
        {
            _mediator = mediator;
            _repositoryUsuario = repositoryUsuario;
        }

        public async Task<Response> Handle(AdicionarUsuarioRequest request, CancellationToken cancellationToken)
        {
            if (request==null)
            {
                AddNotification("Request", "Informe os dados do usuario.");
                return new Response(this);
            }
            if (_repositoryUsuario.Existe(x => x.Email==request.Email))
            {
                AddNotification("Email", "E-mail já cadastrado no sistema.");
                return new Response(this);
            }

            Entities.Usuario usuario = new Entities.Usuario(request.PrimeiroNome, request.UltimoNome, request.Email, request.Senha);
            AddNotifications(usuario);

            if (IsInvalid())
            {
                return new Response(this);
            }
            usuario = _repositoryUsuario.Adicionar(usuario);

            var response = new Response(this, usuario);

            AdicionarUsuarioNotification adicionarUsuarioNotification = new AdicionarUsuarioNotification(usuario);

            await _mediator.Publish(adicionarUsuarioNotification);
            
            return await Task.FromResult(response);
        }
    }
}