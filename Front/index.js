async function GetResultados(valor){
    const apiUrl = `https://localhost:7140/api/Busca/getBusca/?busca=${valor}`;

    let divResultados = document.getElementById("resultados");
    divResultados.innerHTML = "";

    fetch(apiUrl)
    .then(response => {return response.json()})
    .then(data => {
        let div = document.getElementById("resultados");
        data.forEach(resultado =>{
            var h3 = document.createElement('h3');
            h3.innerText = resultado.titulo;

            var a = document.createElement('a');
            a.href = resultado.link;
            a.innerText = `[${a.getAttribute("href")}]`;
            a.innerText = a.innerText.substring(a.innerText.indexOf('/') + 2);
            a.innerText = `[${a.innerText.substring(0, a.innerText.indexOf('/'))}]`;

            var divFilha = document.createElement('div');
            divFilha.className = 'resultado';

            divFilha.appendChild(h3);
            divFilha.appendChild(a);

            div.appendChild(divFilha);
        });
    });
}