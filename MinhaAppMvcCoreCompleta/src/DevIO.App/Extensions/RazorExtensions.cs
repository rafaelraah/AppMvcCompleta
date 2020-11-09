using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormataDocumento(this RazorPage page, int tipoPessoa, string documento)
        {
            string documentoSemCaracteres = documento.Replace(".", "").Replace("-", "").Replace("/", "");
            ulong ulongParaFormatacao = Convert.ToUInt64(documentoSemCaracteres);
            return tipoPessoa == 1 ? ulongParaFormatacao.ToString(@"000\.000\.000\-00") : ulongParaFormatacao.ToString(@"00\.000\.000\/000\-00");
        }
    }
}