const myForm = document.getElementById('cadastroUser');

myForm.addEventListener('submit', function (event) {
    // 1. Prevenir o recarregamento da página ao submeter form
    event.preventDefault();

    fetch('http://localhost:5142/usuario/cadastrar', {
        method: 'POST', //Para outros métodos, basta alterar aqui. Obs: Delete remove a parte do body e headers, e no get é conforme todos os exemploes feitos na Unidade interação com API 
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            nome: document.getElementById("nome").value,
            email: document.getElementById("email").value,
            senha: parseInt(document.getElementById("senha").value)
        }),
    })
    .then(response => response.json())
        .then(data => {
            console.log(data);
            document.getElementById("respostaUser").innerHTML ="<h4>Usuário cadastrado com sucesso! <br>"
            +"Seu ID gerado foi: "+data.id+"</h4>";        
        })
});