using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {

        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;


        public FornecedorService(IFornecedorRepository fornecedorRepository, 
                                    IEnderecoRepository enderecoRepository, 
                                    INotificador notificador) : base(notificador)
        {
            this._fornecedorRepository = fornecedorRepository;
            this._enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Fornecedor fornecedor)
        {
            /**OLD 
            //Validar o estado da entidade
            var validator = new FornecedorValidation();
            var result = validator.Validate(fornecedor);
            if(!result.IsValid) { result.Errors; //a lista de erros, caso quisesse disparar para algum lugar.}
            */

            //Validar o estado da entidade
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor) ||
                !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco))
            {
                return;
            }

            //Um dia interessante neste momento é avaliar se nestas validações terá muitos "Ifs", se sim, 
            //verificar a possiblidade de passar essas regras para um método, deixar tudo encapsulado na 
            //classe do fornecedor, uma classe de validação ou afins 
            if(_fornecedorRepository.Buscar(predicate: f=>f.Documento == fornecedor.Documento).Result.Any()){
                Notificar(mensagem: "Já existe um fornecedor com este documento informado");
                return;
            }

            await _fornecedorRepository.Adicionar(fornecedor);

        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

            //Já existe um outro fornecedor com o novo documento informado
            if (_fornecedorRepository.Buscar(predicate: f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result.Any())
            {
                Notificar(mensagem: "Já existe um fornecedor com este documento informado");
                return;
            }

            await _fornecedorRepository.Atualizar(fornecedor);

        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco); 

        }



        public async Task Remover(Guid id)
        {
            if (_fornecedorRepository.ObterFornecedorProdutosEndereco(id).Result.Produtos.Any())
            {
                Notificar(mensagem: "O fornecedor possui produtos cadastrados!");
                return;
            }

            await _fornecedorRepository.Remover(id);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }

    }

}
