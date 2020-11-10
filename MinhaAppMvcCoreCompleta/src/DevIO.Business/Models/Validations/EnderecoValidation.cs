using FluentValidation;

namespace DevIO.Business.Models.Validations
{
    public class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation()
        {
            string txtObrigatoriedade = "O campo {PropertyName} precisa ser fornecido";
            string txtMinMax = "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres";
            string txtExact = "O campo {PropertyName} precisa ter {MaxLength} caracteres";

            RuleFor(f => f.Logradouro)
                .NotEmpty().WithMessage(txtObrigatoriedade)
                .Length(min: 2, max: 200)
                .WithMessage(txtMinMax);

            RuleFor(f => f.Bairro)
                .NotEmpty().WithMessage(txtObrigatoriedade)
                .Length(min: 2, max: 100)
                .WithMessage(txtMinMax);

            RuleFor(f => f.Cep)
                .NotEmpty().WithMessage(txtObrigatoriedade)
                .Length(8)
                .WithMessage(txtExact);

            RuleFor(f => f.Cidade)
                .NotEmpty().WithMessage(txtObrigatoriedade)
                .Length(min: 2, max: 100)
                .WithMessage(txtMinMax);

            RuleFor(f => f.Estado)
                .NotEmpty().WithMessage(txtObrigatoriedade)
                .Length(min: 2, max: 50)
                .WithMessage(txtMinMax);
            
            RuleFor(f => f.Numero)
                .NotEmpty().WithMessage(txtObrigatoriedade)
                .Length(min: 1, max: 50)
                .WithMessage(txtMinMax);

        }
    }


}