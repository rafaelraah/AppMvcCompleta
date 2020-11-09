using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.App.Extensions
{
    public class MoedaAttribute : ValidationAttribute
    {

        //Esta validação é apenas para o backend.
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            try
            {
                var moeda = Convert.ToDecimal(value, new CultureInfo("pt-BR"));
            }
            catch (Exception)
            {
                return new ValidationResult("Moeda em formato inválido");
            }

            //return base.IsValid(value, validationContext);
            return ValidationResult.Success;
        }

    }

    public class MoedaAttributeAdapter : AttributeAdapterBase<MoedaAttribute>
    {
        public MoedaAttributeAdapter(MoedaAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
        {
        }

        //Já esta daqui, funcionará do lado cliente.
        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-moeda", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-number", GetErrorMessage(context));
        }

        //Sobrescrevi também o método da mensagem de erro, pois senão, utilizaria uma padrão (ou talvez a que foi informada na class MoedaAttribute
        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return "Moeda em formato inválido";
        }

    }

    //Para usar o MoedaAttributeAdapter, é preciso criar um provider
    public class MoedaValidationAtrributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if(attribute is MoedaAttribute moedaAttribute)
            {
                return new MoedaAttributeAdapter(moedaAttribute, stringLocalizer);
            }
            return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }

}
