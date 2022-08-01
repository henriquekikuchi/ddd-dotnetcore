using System.Diagnostics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VemDeZap.Domain.Commands.Usuario.AdicionarUsuario.Notifications
{
    public class EnviarEmailDeAtivacaoDoUsuario : INotificationHandler<AdicionarUsuarioNotification>
    {
        public async Task Handle(AdicionarUsuarioNotification notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Enviar email de ativação para o usúario: " + notification.Usuario.PrimeiroNome);
        }
    }
}