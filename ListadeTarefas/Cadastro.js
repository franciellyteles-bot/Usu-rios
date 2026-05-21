const myForm = document.getElementById('Cadastro');

myForm.addEventListener('submit', function (event) {

    event.preventDefault();

    fetch('https://localhost:7259/Pessoa', {
        method: 'POST', //Para outros métodos, basta alterar aqui. Obs: Delete remove a parte do body e headers, e no get é conforme todos os exemploes feitos na Unidade interação com API 
        
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            nome: document.getElementById("nome").value,
            email: document.getElementById("email").value,
            senha: document.getElementById("senha").value
        }),
    }).then(response => { 
        console.log(response);
        response.json();})
        .then(data => {
            window.location.href='Login.html';
                  
        })

    })
