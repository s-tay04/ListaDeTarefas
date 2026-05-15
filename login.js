const loginForm = document.getElementById('formLogin'); // ID do seu formulário de login

loginForm.addEventListener('submit', function (event) {
    // 1. Prevenir o recarregamento da página
    event.preventDefault();

    // 2. Capturar os dados dos inputs
    const emailInput = document.getElementById("emailLogin").value;
    const senhaInput = document.getElementById("senhaLogin").value;

    fetch('http://localhost:5011/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            email: emailInput,
            senha: senhaInput
        }),
    })
    .then(response => {
        // Diferente do cadastro, aqui checamos se o status é 200 (Ok) ou 401 (Unauthorized)
        if (response.ok) {
            return response.text(); // O backend retorna uma string "Login realizado com sucesso"
        } else {
            // Se cair aqui, é porque o login falhou (Unauthorized)
            throw new Error("E-mail ou senha inválidos");
        }
    })
    .then(data => {
        // Caso de sucesso
        document.getElementById("respostaLogin").innerHTML = `<h4 style="color: green;">${data}</h4>`;
        
        // Opcional: Redirecionar o usuário após 2 segundos
        // setTimeout(() => { window.location.href = "home.html"; }, 2000);
    })
    .catch(error => {
        document.getElementById("respostaLogin").innerHTML = `<h4 style="color: red;">${error.message}</h4>`;
    });
});