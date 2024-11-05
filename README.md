# Library

![GitHub repo size](https://img.shields.io/github/repo-size/LauanAmorim/Library)

> Esse projeto foi feito com o objetivo de desenvolver e praticar conceitos de Clean Architeture, DDD, CQRS, Mediator e Repository.

### Ajustes e melhorias

O projeto ainda está em desenvolvimento e as próximas atualizações serão voltadas para as seguintes tarefas:

- [ ] Testes unitários (Somente nos recursos de POST)
	- [x] BookController
	- [ ] useCases
	- [ ] Persistência

## 💻 Pré-requisitos

Antes de começar, verifique se você atendeu aos seguintes requisitos:

- SDK do .NET 8.
- Ler o guia de instalação do projeto.

## 🚀 Instalando o projeto Library

Para instalar o projeto Library, siga estas etapas:

Instale o SDK do [.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0) (Windows):

```shell
winget install Microsoft.DotNet.SDK.8
```

Clone o repositório:

```shell
git clone https://github.com/LauanAmorim/Library.git
```

## ☕ Usando o projeto Library

Para usar o projeto Library, siga estas etapas:

Na raiz do projeto `./Library` execute:

```csharp
dotnet build
```

em `./Library/Library.API` execute:

```csharp
dotnet watch run
```

Para executar os testes:

```csharp
dotnet test
```

