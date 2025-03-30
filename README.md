# 📝 API de Tarefas (ASP.NET Core 6)

Esta é uma API RESTful para gerenciamento de tarefas, desenvolvida em ASP.NET Core 6. A API permite criar, listar, filtrar, editar e excluir tarefas, com suporte a validações, documentação Swagger e testes automatizados com xUnit.

---

## 📁 Estrutura do Projeto

```
TarefasAPI/
├── Controllers/
├── Services/
├── Repositories/
├── Models/
├── Data/
├── DTOs/
├── Middlewares/
├── Program.cs
└── README.md
TarefasAPI.Tests/
```

---

## 🚀 Como rodar o projeto

### Pré-requisitos

- .NET 6 SDK instalado
- Visual Studio 2022 ou VS Code

### Passos

1. Clone o repositório:

```bash
git clone https://github.com/leonardomendes201704/TarefasAPI.git
```

2. Acesse a pasta:

```bash
cd TarefasAPI
```

3. Restaure os pacotes:

```bash
dotnet restore
```

4. Rode a aplicação:

```bash
dotnet run
```

5. Acesse a documentação no navegador:

```
https://localhost:5001/swagger
```

---

## 🧪 Como rodar os testes

A solução inclui testes automatizados com **xUnit** na camada de serviços.

### Comando para executar os testes:

```bash
dotnet test
```

Os testes ficam no projeto `TarefasAPI.Tests`.

---

## ✅ Funcionalidades

- [x] Criar tarefas (`POST /api/tarefas`)
- [x] Listar todas as tarefas (`GET /api/tarefas`)
- [x] Filtrar tarefas por status e/ou data (`GET /api/tarefas/GetFiltered`)
- [x] Buscar tarefa por ID (`GET /api/tarefas/{id}`)
- [x] Atualizar tarefa (`PUT /api/tarefas/{id}`)
- [x] Excluir tarefa (`DELETE /api/tarefas/{id}`)

---

## 📁 Tecnologias Utilizadas

- ASP.NET Core 6
- Entity Framework Core (InMemory)
- Swagger / Swashbuckle
- xUnit
- Moq
- SOLID + Clean Architecture
- Logging com ILogger
- Middleware para tratamento global de erros

---

## 📂 DTOs Utilizados

- `TarefaCreateDto`: usado para criação de tarefas via POST
- `StatusTarefa`: enum com valores válidos (`Pendente`, `EmProgresso`, `Concluida`)

---

## 📚 Padrões adotados

- Padrão RESTful
- Princípios SOLID
- DTOs para entrada/saída de dados
- Middleware para tratamento de exceções
- Injeção de dependência nativa do ASP.NET Core

---

## ✍️ Autor

**Leonardo Mendes**

---
