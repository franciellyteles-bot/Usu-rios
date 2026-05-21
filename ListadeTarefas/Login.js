const myForm = document.getElementById('loginCliente');

myForm.addEventListener('submit', function (event) {

    event.preventDefault();

    fetch('https://localhost:7259/Pessoa/Login', {
        method: 'POST', //Para outros métodos, basta alterar aqui. Obs: Delete remove a parte do body e headers, e no get é conforme todos os exemploes feitos na Unidade interação com API 
        credentials:'include',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            nome:"",
            email: document.getElementById("email").value,
            senha: document.getElementById("senha").value
        }),
     }).then(response => {
        if (response.status == 401) {
            alert("Email ou senha Incorretos!");
        } else {
            alert("Logado com sucesso");
            window.location.href = "Home.html";
        }
    })

});

