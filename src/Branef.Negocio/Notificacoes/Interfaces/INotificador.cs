using System.Collections.Generic;
using Branef.Negocio.Notificacoes;

namespace Branef.Negocio.Notificacoes.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}