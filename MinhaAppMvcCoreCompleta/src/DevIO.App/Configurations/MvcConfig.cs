using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DevIO.App.Configurations
{
    public static class MvcConfig
    {

        public static IServiceCollection AddMvcConfig(this IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor preenchido não é válido para este campo.");
                o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Este campo precisa ser preenchido.");
                o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Este campo precisa ser preenchido.");
                o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
                o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "O valor não é válido.");
                o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor fornecido é inválido.");
                o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser numérico");
                o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "O valor fornecido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "O valor fornecido é inválido para {0}.");
                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "O campo {0} deve ser númerico.");
                o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "Este campo precisa ser preenchido");

                //Validando automaticamente o atributo [ValidateAntiForgeryToken] em todos os requests 
                //Evitar ataques CSRF
                //Obs: O asp net core já é bem seguro em relação à ataques, faz filtros..
                //..automaticamente de injeção de Script (ataque XSS), tem uma proteção natural para o SQL Injection.. etc
                o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;

        }

    }

}
