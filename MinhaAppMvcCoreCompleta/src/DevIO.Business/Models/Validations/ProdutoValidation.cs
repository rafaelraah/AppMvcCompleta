using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            string txtObrigatoriedade = "O campo {PropertyName} precisa ser fornecido";
            string txtMinMax = "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";
            string txtGreaterThan = "O campo {PropertyName} precisa ser maior que {ComparisonValue}";

            RuleFor(p => p.Nome)
                .NotEmpty().WithMessage(txtObrigatoriedade)
                .Length(min: 2, max: 200)
                .WithMessage(txtMinMax);

            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage(txtObrigatoriedade)
                .Length(min: 2, max: 1000)
                .WithMessage(txtMinMax);

            RuleFor(p => p.Valor)
                .GreaterThan(0)
                .WithMessage(txtGreaterThan);
        }
    }


}