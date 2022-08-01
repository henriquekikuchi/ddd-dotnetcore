using System.Diagnostics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VemDeZap.Domain.Commands.Usuario.AdicionarUsuario.Notifications
{
    public class AvisarAdministradores : INotificationHandler<AdicionarUsuarioNotification>
    {
        public async Task Handle(AdicionarUsuarioNotification notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Chamar web service que avisa que um novo usuario se cadastrou: " + notification.Usuario.PrimeiroNome);
        }
    }
}