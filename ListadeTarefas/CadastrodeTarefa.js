const myForm1 = document.getElementById('novaTarefa');
if (myForm1 != null) {
myForm1.addEventListener('submit', function (event) {
    // 1. Prevenir o recarregamento da página ao submeter form
    event.preventDefault();

    fetch('https://localhost:7259/Tarefa', {
        method: 'POST', //Para outros métodos, basta alterar aqui. Obs: Delete remove a parte do body e headers, e no get é conforme todos os exemploes feitos na Unidade interação com API 
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            descricao: document.getElementById("descricao").value,
            status: document.getElementById("status").value,
            
        }),
    }).then(response => {
        if (response.status ==401){
            alert ("Faça login antes de Cadastrar");
            window.location.href="Home.html";
        }
        response.json();})
        .then(data => {
            document.getElementById("respostaTarefa").innerHTML ="<h4>Tarefa cadastrada com sucesso!</h4>";        
        })
});
}

fetch('https://localhost:7259/Tarefa',
    { 
        credentials: 'include' 
       
    }).then(response => {
        
       return  response.json();})
    .then(data => {
        console.log (data);
        if(data.length >0){
        var resposta = document.getElementById("respostaConsulta");
        resposta.innerHTML = "<h4>Segue Lista de suas Tarefas</h4> ";
        for (i = 0; i < data.length; i++) {
            resposta.innerHTML += "<li> Pessoa: " + data[i].pessoa + "</li>";
            resposta.innerHTML += "Descricao: <input type='text' id='descricao"+data[i].tarefa+"' value='" + data[i].descricao+ "'>";
            resposta.innerHTML += "status: <input type='text' id='status"+data[i].tarefa+"' value='" + data[i].status+ "'>";
            resposta.innerHTML += "<button onclick='editaTarefa("+data[i].tarefa+")'>Editar Tarefa </button>";
            resposta.innerHTML += "<button onclick='deletaTarefa("+data[i].tarefa+")'>Deletar Tarefa </button> <hr>";

        }
    }
    });

    function deletaTarefa(idTarefa){
        fetch('https://localhost:7259/Tarefa/'+idTarefa, {
            method: 'DELETE', 
            credentials: 'include'
  
        }).then(response => {
            alert("Tarefa excluída");
            window.location.href="Home.html";
        })
    }

    function editaTarefa (idTarefa){
        fetch('https://localhost:7259/Tarefa/'+idTarefa, {
            method: 'PUT',   
            credentials: 'include',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                descricao: document.getElementById("descricao"+idTarefa).value,
                status:document.getElementById("status"+idTarefa).value
            }),
        }).then(response => {
            if (response.status ==401){
                alert ("Faça login antes de Cadastrar!");
                window.location.href="Home.html";
            }else{
                alert ("Tarefa editada!");
            }})
           
    }
    function logout() {
        fetch('https://localhost:8152/Pessoas/logout',{credentials: 'include'})
        .then(response =>{
            console.log(response);
            window.location.href = "index.html"
        })
    }