# Task Manager

Aplicação full stack para gerenciamento de tarefas pessoais, desenvolvida como teste técnico de Desenvolvedor Júnior.

## Tecnologias

- **Frontend:** Angular 21
- **Backend:** C# / ASP.NET Core 10 / Entity Framework Core
- **Banco de dados:** PostgreSQL
- **Testes:** xUnit / Moq / FluentAssertions
- **Fluxos:** Node-RED
- **Containerização:** Docker

---

## Arquitetura e Padrões

O projeto utiliza padrões de design para garantir manutenibilidade e testabilidade:

- **Repository Pattern:** Abstrai a lógica de acesso a dados, permitindo que os controladores não dependam diretamente do DbContext.
- **Unit of Work:** Gerencia transações de forma centralizada, garantindo que múltiplas operações no banco de dados sejam tratadas como uma única unidade.

---

## Padronização de Erros (Middleware)

A API implementa um **Global Exception Middleware** para capturar exceções não tratadas. Todas as respostas de erro seguem o padrão **RFC 7807 (Problem Details for HTTP APIs)**, garantindo um formato JSON consistente:

```json
{
  "title": "Internal Server Error",
  "status": 500,
  "detail": "Mensagem detalhada do erro (apenas em ambiente de desenvolvimento)",
  "instance": "/api/task"
}
```

---

## Estrutura do Repositório

```
task-manager/
├── frontend/         # Aplicação Angular
├── backend/          # Solução .NET
│   ├── TaskManager.API     # API Principal
│   └── TaskManager.Tests   # Testes Unitários
├── nodered/          # Fluxos Node-RED
├── database/         # Script SQL da estrutura do banco
└── docker-compose.yml
```

---

## Pré-requisitos

- [Docker Desktop](https://www.docker.com/products/docker-desktop)

Para rodar manualmente (sem Docker):

- [.NET SDK 10+](https://dotnet.microsoft.com/download)
- [Node.js LTS](https://nodejs.org)
- [Angular CLI](https://angular.io/cli)
- [Node-RED](https://nodered.org)

---

## Testes Unitários

Para rodar os testes unitários do backend, navegue até a pasta `backend/` e execute o comando através de um terminal:

```cmd
dotnet test
```

Os testes cobrem as principais funcionalidades dos controladores e garantem que as regras de negócio e integrações com repositórios funcionem conforme o esperado.

---

## Configuração

Antes de rodar o projeto, é necessário criar o arquivo de configuração do backend.

Navegue até `backend/TaskManager.API/`, copie o arquivo de exemplo e renomeie:

```cmd
copy appsettings.example.json appsettings.json
```

Abra o `appsettings.json` e preencha com as suas credenciais:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=taskdb;Username=SEU_USUARIO;Password=SUA_SENHA"
  }
}
```

---

## Migrações

As migrations são aplicadas **automaticamente** ao iniciar o backend — não é necessário rodar nenhum comando adicional para criar as tabelas.

---

## Como Rodar

### Opção 1 – Docker (recomendado)

Na raiz do projeto, utilize o comando pelo terminal:

```cmd
docker-compose up --build
```

| Serviço | URL |
|---|---|
| Frontend | http://localhost:4200 |
| API | http://localhost:8080/api/task |

---

### Opção 2 – Manual

**1. Banco de dados:**

Crie um banco PostgreSQL com as seguintes configurações, seja por instalação local ou pela ferramenta de sua preferência.
Na pasta `database` contém o modelo do comando de criação do Banco de Dados como forma de orientação.
```
Host:     localhost
Port:     5432
Database: taskdb
Username: admin
Password: admin123
```

Caso prefira usar Docker para o Banco de Dados:
```cmd
docker run --name postgres-tasks -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=admin123 -e POSTGRES_DB=taskdb -p 5432:5432 -d postgres
```
Fique atento em utilizar o mesmo Nome de Usuário e Senha do Banco de Dados na Connection String do Backend.

**2. Backend** — abra um terminal em `backend/TaskManager.API/` e rode o comando no terminal:

```cmd
dotnet run
```

**3. Frontend** — abra um segundo terminal em `frontend/task-manager-app/` e rode o comando no terminal:

```cmd
ng serve
```

**4. Node-RED** — abra um terceiro terminal e rode o comando no terminal:

```cmd
node-red
```

Acesse `http://localhost:1880`, clique no menu hamburguer → **Import** e selecione o arquivo `nodered/flows.json`.

| Serviço | URL |
|---|---|
| Frontend | http://localhost:4200 |
| Swagger | https://localhost:{porta}/swagger |
| Broker Catalog | http://localhost:1880/corretoras |
| ZIP Code Searcher | http://localhost:1880/cep |

---

## Rotas da API

Base URL: `http://localhost:8080/api` (Docker) ou `http://localhost:{porta}/api` (manual)

---

### `GET /task`

Lista todas as tarefas.

**Resposta:**
```json
[
  {
    "id": 1,
    "title": "Estudar Angular",
    "description": "Aprofundar conhecimentos sobre a Framework",
    "priority": "High",
    "status": "InProgress",
    "createdAt": "2026-03-20T10:00:00Z"
  }
]
```

---

### `GET /task/{id}`

Retorna uma tarefa específica.

**Resposta:**
```json
{
  "id": 1,
  "title": "Estudar Angular",
  "description": "Aprofundar conhecimentos sobre a Frameworks",
  "priority": "High",
  "status": "InProgress",
  "createdAt": "2026-03-20T10:00:00Z"
}
```

---

### `POST /task`

Cria uma nova tarefa.

**Body:**
```json
{
  "title": "Estudar Angular",
  "description": "Aprofundar conhecimentos sobre a Framework",
  "priority": "High",
  "status": "NotStarted"
}
```

Valores aceitos para `priority`: `Low`, `Normal`, `High`

Valores aceitos para `status`: `NotStarted`, `InProgress`, `Completed`

**Resposta:** `201 Created` com o objeto criado.

---

### `PUT /task/{id}`

Atualiza uma tarefa existente.

**Body:**
```json
{
  "title": "Estudar Angular",
  "description": "Aprofundar conhecimentos sobre a Framework",
  "priority": "High",
  "status": "InProgress"
}
```

**Resposta:** `200 OK` com o objeto atualizado.

---

### `DELETE /task/{id}`

Remove uma tarefa.

**Resposta:** `204 No Content`

---

## Node-RED

Os fluxos são independentes do sistema de tarefas e consomem APIs externas da [BrasilAPI](https://brasilapi.com.br).

| Fluxo | URL | Descrição |
|---|---|---|
| Broker Catalog | `http://localhost:1880/corretoras` | Lista corretoras no formato `Nome - Cidade / CNPJ` |
| ZIP Code Searcher | `http://localhost:1880/cep` | Busca endereço por CEP com campo de pesquisa |