using DevIO.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {

        private readonly INotificador _notificador;

        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificadoes = await Task.FromResult(_notificador.ObterNotificacoes());

            notificadoes.ForEach(action: c => ViewData.ModelState.AddModelError(key: string.Empty, errorMessage: c.Mensagem));

            return View();
        }

    }
}
