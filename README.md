# Backend Challenge

>  This is a challenge by [Coodesh](https://coodesh.com/)

Este projeto é um desafio proposto pela Coodesh que se trata de uma API que utiliza a base do [Free Dictionary API](https://dictionaryapi.dev/) para buscar palavras em inglês.
A API desenvolvida possui suporte a autenticação, histórico de acesso e a favoritar/desfavoritar palavras.

## Tecnologias utilizadas

Foram utilizadas as seguintes tecnologias para o desenvolvimento do projeto:

- API
    - ASP.NET (.NET 6)
    - C#
- Banco de dados
    - Postgres (hospedado na Heroku)
    - Entity Framework Core (Code First)
- Cache
    - ASP.NET MemoryCache
- Autenticação
    - Bearer JWT
- Documentação
    - Swagger
- Hospedagem
    - Docker

## Como utilizar a API online

Para ter acesso ao `Swagger` de maneira online, basta acessar a seguinte URL:
```
http://38.242.195.152:5193/swagger
```

## Como executar o projeto localmente

### Método nativo

Para executar o projeto de forma nativa, realize o clone do projeto e execute o seguinte comando dentro de `.\src\BackendChallenge.API`:
```
dotnet run
```

**Tenha certeza de que possua pelo menos o `.NET Runtime 6.0` instalado na máquina.** Para baixá-lo, utilize este [link](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) e realize o download de acordo com o sistema operacional.

Após isso, basta abrir no navegador a `URL localhost` na porta indicada e o retorno inicial irá aparecer.

### Docker

Para executar o projeto com docker, realize o clone do projeto e execute o seguinte comando dentro da pasta raíz do projeto:
```
docker compose up
```

**Tenha certeza de que possua o `Docker` de seu sistema operacional instalado na máquina.**

Após isso, basta abrir no navegador a `URL localhost` na porta `5193` e o retorno inicial irá aparecer.

## Swagger

Para ter acesso ao `Swagger`, basta abrir a URL indicada pela execução mostrada na seção anterior utilizando `/swagger`. Exemplo:
```
http://localhost:5193/swagger
```