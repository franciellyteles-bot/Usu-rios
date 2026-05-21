const myForm = document.getElementById('Home');

myForm.addEventListener('submit', function (event) {

    event.preventDefault();

    fetch('https://localhost:7259/Home', {
        method: 'POST', //Para outros métodos, basta alterar aqui. Obs: Delete remove a parte do body e headers, e no get é conforme todos os exemploes feitos na Unidade interação com API 
        credentials:'include',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            MinhasTraefas: document.getElementById("MinhasTarefas").value,
            AdicionarTrarefas: document.getElementById("AdicionarTarefas").value,
        }),
    }).then(response => response.json())
        .then(data => {
            document.getElementById("RespostaAdicionarTarefas").innerHTML ="<h4>cadastrado com sucesso! <br></h4>"
                  
        })
});