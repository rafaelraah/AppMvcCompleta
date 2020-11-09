function AjaxModal() {
   // alert(1);
    $(document).ready(function () {

        //alert(2);
        $(function () {
            $.ajaxSetup({ cache: false });
          
            $("a[data-modal").on("click", function (e) {
             
                $("#myModalContent").load(this.href, function () {
               
                    $('#myModal').modal({
                        keyboard: true
                    }, 'show');
                    
                    bindForm(this);
                });
                return false;
            });
        });


        function bindForm(dialog) {
            $('form', dialog).submit(function () {
               
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        if (result.success) {
                            $('#myModal').modal('hide');
                            $('#EnderecoTarget').load(result.url); //Carrega o resultado html para a div demarcada
                        } else {
                            $('#myModalContent').html(result);
                            bindForm(dialog);
                        }
                    }
                });
                return false;
            });
        }
    });
}


function BuscaCep() {
    $(document).ready(function () {
        function limpa_formulario_cep() {
            $("#Endereco_Logradouro").val("");
            $("#Endereco_Bairro").val("");
            $("#Endereco_Cidade").val("");
            $("#Endereco_Estado").val("");
        }

        //Quando perder o foco
        $("#Endereco_Cep").blur(function () {
            //Nova variável "cep" somento com dígitos.
            var cep = $(this).val().replace(/\D/g, '');

            if (cep != "") {

                var validacep = /^[0-9]{8}$/;

                if (validacep.test(cep)) {
                    $("#Endereco_Logradouro").val("...");
                    $("#Endereco_Bairro").val("...");
                    $("#Endereco_Cidade").val("...");
                    $("#Endereco_Estado").val("...");

                    //Consulta no webservice viacep.com.br
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {
                            if (!("erro" in dados)) {
                                $("#Endereco_Logradouro").val(dados.logradouro);
                                $("#Endereco_Bairro").val(dados.bairro);
                                $("#Endereco_Cidade").val(dados.localidade);
                                $("#Endereco_Estado").val(dados.uf);
                            } else {
                                limpa_formulario_cep();
                                alert("CEP não encontrado");
                            }
                        }
                    );
                } else {
                    limpa_formulario_cep();
                    alert("Formato de CEP inválido");
                }

            } else {
                limpa_formulario_cep();
            }

        });


    });
}

$(document).ready(function () {
    $("#msg_box").fadeOut(2500);
});