# Orders Management

Este projeto consiste em uma aplicação de gerenciamento de pedidos, seguindo práticas de arquitetura limpa (Clean Architecture). A solução envolve cadastro de revendas, recebimento de pedidos de clientes e emissão de pedidos para uma API externa (simulada, devido à instabilidade).

---

## 📦 Tecnologias Utilizadas

- **.NET 9** (ASP.NET Core)
- **Entity Framework Core** (PostgreSQL)
- **PostgreSQL** (Banco de dados relacional)
- **JWT** (Autenticação via token) // TODO: não tive tempo de concluir
- **Swagger** (Documentação da API)
- **Clean Architecture** (Separação em camadas)
- **xUnit / Moq** (Testes unitários)

---

## 🧠 Decisões Arquiteturais

- **Camadas**:
  - `Domain`: Entidades, enums e value objects.
  - `Application`: Contratos, Interfaces...
  - `Infrastructure`: Repositórios, EF Core.
  - `Web`: API HTTP com controllers, configuração e Swagger.

- **Pedidos Consolidados**:
  - Recebe pedidos de clientes (PedidoCliente) e consolida em um PedidoCentral.
  - Pedido mínimo de 1000 unidades antes de enviar à API da Ambev.

- **Resiliência e Retry**:
  - Pedidos que não são enviados devido à falha da API externa são salvos com `Status = EmEspera`.
  - Um `BackgroundService` tenta reenviar periodicamente (com `Polly`).

- **Value Objects**:
  - Validações fortes para CNPJ.

---

## 🗂️ Estrutura do Projeto

```
OrdersManagement.sln
├── OrdersManagement.Domain         # Entidades e Value Objects
├── OrdersManagement.Application    # Casos de uso e serviços de aplicação
├── OrdersManagement.Infrastructure # EF Core, repositórios
└── OrdersManagement.Web            # Controllers, Swagger
```

---

## ⚙️ Configuração e Execução

### 1. Clone o projeto

```bash
git clone https://github.com/seu-usuario/orders-management.git
cd orders-management
```

### 2. Configure o banco

Altere a connection string em `OrdersManagement.Web/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=orders_db;Username=postgres;Password=secret"
}
```

### 3. Rode as migrations

```bash
dotnet ef database update --project OrdersManagement.Infrastructure --startup-project OrdersManagement.Web
```

### 4. Execute a aplicação

```bash
dotnet run --project OrdersManagement.Web
```

Acesse: [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## ✅ Testes

### Rodar testes unitários

```bash
dotnet test
```

Estrutura de testes:

- `OrdersManagement.UnitTests`: lógica de domínio e serviços.

---

## 🔁 Fluxo de Pedidos

1. **Cadastro de Revendas**
   - CRUD de revendas com CNPJ, razão social, etc.

2. **Pedidos de Clientes**
   - Endpoint `POST /revenda/{id}/pedido-cliente`

3. **Pedido Central**
   - Endpoint `POST /revenda/{id}/pedido-central`
   - Valida se soma de produtos >= 1000 unidades.
   - Tenta enviar para a Central.
   - Se API falhar, salva como `EmEspera`.

4. **Retry automático**
   - Serviço em segundo plano tenta reenviar pedidos `EmEspera` até sucesso.

---

## ✅ Validações

- CNPJ: validado via Value Object.
- Pedido Central: exige 1000+ unidades.
- Email/CEP/Telefone: validados no domínio.
