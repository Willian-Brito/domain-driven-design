using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Modules.Core.Communication.Mediator;
using NerdStore.Modules.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.WebApp.Controllers.Base;

public abstract class ControllerBase : Controller
{
    private readonly DomainNotificationHandler _notifications;
    private readonly IMessageBus _messageBus;

    protected Guid ClienteId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

    protected ControllerBase(
        INotificationHandler<DomainNotification> notifications,
        IMessageBus messageBus
    )
    {
        _notifications = (DomainNotificationHandler)notifications;
        _messageBus = messageBus;
    }

    protected bool OperacaoValida()
    {
        return !_notifications.TemNotificacao();
    }

    protected IEnumerable<string> ObterMensagensErro()
    {
        return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
    }

    protected void NotificarErro(string codigo, string mensagem)
    {
        _messageBus.PublicarNotificacao(new DomainNotification(codigo, mensagem));
    }
}